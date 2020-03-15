using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.UI.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcCarrier : FilterUserControlPage
    {
        private ObservableCollection<Carrier> _carriers;
        private ObservableCollection<CarrierService> _carrierServices;
        private Carrier _activeCarrier;

        public UcCarrier()
        {
            InitializeComponent();

            UserControlTitle = "پیک";
            ImagePath = "Icons/NavigationBar/Carrier_Small.svg";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchComboBox();
            LoadSearchGridControlData();
            IsNotEditingAndAdding();
        }

        private void LoadSearchComboBox()
        {
            var statuses = new List<Tuple<bool, string>>();
            statuses.Add(new Tuple<bool, string>(false, "غیر فعال"));
            statuses.Add(new Tuple<bool, string>(true, "فعال"));
            SearchStatusComboBoxEdit.ItemsSource = statuses;
        }

        private void LoadSearchGridControlData(string searchTitle = null, object searchStatus = null)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var carrierQueryable = dbContext.Carriers.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(searchTitle))
                    carrierQueryable = carrierQueryable.Where(item => item.Title.Contains(searchTitle));
                if (searchStatus != null)
                {
                    var searchStatusIsActive = searchStatus as Tuple<bool, string>;
                    carrierQueryable = carrierQueryable.Where(item => item.IsActive == searchStatusIsActive.Item1);
                }
                var totalRecordsCount = carrierQueryable.Count();
                _carriers = carrierQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _carriers;
                PaginationUserControl.UpdateRecordsDetail(_carriers.Count, totalRecordsCount);
            }
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchStatusComboBoxEdit.SelectedItemValue);
        }

        protected override void OnAddToolBarItem()
        {
            _activeCarrier = new Carrier { IsActive = true };
            EditData(_activeCarrier);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeCarrier);
            if (_activeCarrier.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var carrier = dbContext.Carriers.Find(_activeCarrier.Id);
                    var carrierServices = dbContext.CarrierServices
                        .Where(item => item.CarrierId == carrier.Id);

                    foreach (var carrierService in carrierServices)
                    {
                        var tempCarrier = _carrierServices.FirstOrDefault(item => item.Id == carrierService.Id);

                        if (tempCarrier == null)
                            dbContext.CarrierServices.Remove(carrierService);
                        else
                        {
                            carrierService.Title = tempCarrier.Title;
                            carrierService.Code = tempCarrier.Code;
                            dbContext.Entry(carrierService).State = EntityState.Modified;
                        }
                    }

                    foreach (var carrierService in _carrierServices.Where(item => item.Id <= 0))
                    {
                        carrierService.Carrier = carrier;
                        dbContext.CarrierServices.Add(carrierService);
                    }

                    carrier.Title = _activeCarrier.Title;
                    carrier.Scac = _activeCarrier.Scac;
                    carrier.Description = _activeCarrier.Description;
                    carrier.IsActive = _activeCarrier.IsActive;
                    dbContext.SaveChanges();

                    IsEditing();
                }
            }
            else
            {
                _activeCarrier.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activeCarrier.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    _activeCarrier.CarrierServices.AddRange(_carrierServices);
                    dbContext.Carriers.Add(_activeCarrier);
                    dbContext.SaveChanges();
                }

                OnAddToolBarItem();
            }

            MessageBoxService.SaveConfirmation(_activeCarrier.Title);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeCarrier.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var carrier = dbContext.Carriers.Find(_activeCarrier.Id);
                    dbContext.Carriers.Remove(carrier);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeCarrier = new Carrier();
                IsNotEditingAndAdding();
            }
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            PaginationUserControl.CurrentPage = 1;
            LoadSearchGridControl();
        }

        private void SearchGridControlOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var searchGridControl = (GridControl)sender;
            var rowIndex = searchGridControl.View.GetRowHandleByMouseEventArgs(e);
            if (rowIndex == DataControlBase.InvalidRowHandle)
                return;
            if (searchGridControl.ItemsSource is ObservableCollection<Carrier> carriers)
                EditData(carriers[rowIndex]);
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var carrier = SearchGridControl.SelectedItem as Carrier;
            EditData(carrier);
        }

        private void EditData(Carrier carrier)
        {
            _activeCarrier = carrier;
            LoadCarrierServices(_activeCarrier.Id);
            FillData(_activeCarrier);
            IsEditing();
        }

        private void LoadCarrierServices(int carrierId)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                _carrierServices = new ObservableCollection<CarrierService>(
                    dbContext.CarrierServices.Where(item => item.CarrierId == carrierId).ToList());
            }
        }

        private void FillData(Carrier carrier)
        {
            TitleTextEdit.Text = carrier.Title;
            ScacTextEdit.Text = carrier.Scac;
            DescriptionTextEdit.Text = carrier.Description;
            IsActiveCheckEdit.EditValue = carrier.IsActive;
            CarrierServiceGridControl.ItemsSource = _carrierServices;
        }

        private void ReadData(Carrier carrier)
        {
            carrier.Title = TitleTextEdit.Text;
            carrier.Scac = ScacTextEdit.Text;
            carrier.Description = DescriptionTextEdit.Text;
            carrier.IsActive = Convert.ToBoolean(IsActiveCheckEdit.EditValue);
        }

        private void AddItemButtonOnClick(object sender, RoutedEventArgs e)
        {
            var newCarrierService = new CarrierService();
            var newUc = new UcCarrierService(newCarrierService);
            WindowService.OpenUserControlDialog(newUc);
            var carrierService = ApplicationDataStore.GetData<CarrierService>("CarrierService");
            if (carrierService != null)
                _carrierServices.Add(carrierService);
            CarrierServiceGridControl.RefreshData();
        }

        private void EditItemButtonOnClick(object sender, RoutedEventArgs e)
        {
            var activeCarrierService = CarrierServiceGridControl.SelectedItem as CarrierService;
            var editUc = new UcCarrierService(activeCarrierService);
            WindowService.OpenUserControlDialog(editUc);
            var carrierService = ApplicationDataStore.GetData<CarrierService>("CarrierService");
            if (carrierService != null)
            {
                var oldOne = _carrierServices.First(item => item.Id == carrierService.Id);
                _carrierServices.Remove(oldOne);
                _carrierServices.Add(carrierService);
            }
            CarrierServiceGridControl.RefreshData();
        }

        private void DeleteItemButtonOnClick(object sender, RoutedEventArgs e)
        {
            var activeCarrierService = CarrierServiceGridControl.SelectedItem as CarrierService;
            if (MessageBoxService.AskForDelete(activeCarrierService.Title) == true)
                _carrierServices.Remove(activeCarrierService);
        }

        protected override void OnAdding()
        {
            TitleTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(UserControlTitle);
        }

        protected override void OnEditing()
        {
            TitleTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeCarrier.Title);
        }

        protected override void OnNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInactiveHeaderTitle(UserControlTitle);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }
    }
}
