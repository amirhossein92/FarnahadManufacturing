using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.Input;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcLocation : FilterUserControlBase
    {
        private ObservableCollection<Location> _locations;
        private Location _activeLocation;

        public UcLocation()
        {
            InitializeComponent();

            UserControlTitle = "محل";
            ImagePath = "Icons/NavigationBar/Location_Small.svg";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchStatusComboBox();
            LoadSearchGridControl();

            LoadLocationGroupComboBox();
            LoadLocationTypeComboBox();
            LoadCustomerComboBox();
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchDescriptionTitleTextEdit.Text,
                SearchLocationGroupComboBoxEdit.EditValue, SearchStatusComboBoxEdit.EditValue,
                SearchLocationNumberSpinEdit.EditValue);
        }

        private void LoadSearchGridControlData(string title, string description,
            object locationGroup, object statusComboBox, object locationNumber)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var locationQueryable = dbContext.Locations.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    locationQueryable = locationQueryable.Where(item => item.Title.Contains(title));
                if (!string.IsNullOrEmpty(description))
                    locationQueryable = locationQueryable.Where(item => item.Description.Contains(description));
                if (locationGroup is int locationGroupId)
                    locationQueryable = locationQueryable.Where(item => item.LocationGroupId == locationGroupId);
                if (statusComboBox is bool status)
                    locationQueryable = locationQueryable.Where(item => item.IsActive == status);
                if (locationNumber != null)
                {
                    var locationNumberValue = Convert.ToInt32(locationNumber);
                    if (locationNumberValue != 0)
                        locationQueryable = locationQueryable.Where(item => item.Number == locationNumberValue);
                }
                var totalRecordsCount = locationQueryable.Count();
                _locations = locationQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _locations;
                PaginationUserControl.UpdateRecordsDetail(_locations.Count, totalRecordsCount);
            }
        }

        private void LoadSearchStatusComboBox()
        {
            var statuses = new ObservableCollection<FmComboModel<bool>>
            {
                new FmComboModel<bool>(false, "غیر فعال"),
                new FmComboModel<bool>(true, "فعال"),
            };
            SearchStatusComboBoxEdit.ItemsSource = statuses;
        }

        private void LoadLocationGroupComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var locationGroups = dbContext.LocationGroups
                    .Select(item => new FmComboModel<int> { Title = item.Title, Value = item.Id })
                    .ToList();
                SearchLocationGroupComboBoxEdit.ItemsSource = locationGroups;
                LocationGroupComboBoxColumn.ItemsSource = locationGroups;
                LocationGroupComboBoxEdit.ItemsSource = locationGroups;
            }
        }

        private void LoadLocationTypeComboBox()
        {
            // TODO: Complete enum lists
            var locationTypes = new ObservableCollection<FmComboModel<LocationType>>
            {
                new FmComboModel<LocationType>(LocationType.Consignment,"Consignment"),
                new FmComboModel<LocationType>(LocationType.Inspection,"بررسی"),
                new FmComboModel<LocationType>(LocationType.Locked,"انبار ایمن"),
                new FmComboModel<LocationType>(LocationType.Manufacturing,"کارخانه (محل تولید)"),
                new FmComboModel<LocationType>(LocationType.Picking,"انبار محصولات آماده برداشتن"),
                new FmComboModel<LocationType>(LocationType.Receiving,"انبار قطعات و کالای خریداری شده"),
                new FmComboModel<LocationType>(LocationType.Shipping,"انبار محصولات آماده ارسال"),
                new FmComboModel<LocationType>(LocationType.Stock,"انبار"),
                new FmComboModel<LocationType>(LocationType.StoreFront,"فروشگاه"),
                new FmComboModel<LocationType>(LocationType.Vendor,"انبار محصولات فروخته شده"),
            };

            LocationTypeComboBoxEdit.ItemsSource = locationTypes;
        }

        private void LoadCustomerComboBox()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var customers = dbContext.Customers
                    .Select(item => new FmComboModel<int> { Title = item.Title, Value = item.Id })
                    .ToList();
                DefaultCustomerComboBoxEdit.ItemsSource = customers;
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activeLocation = new Location();
            FillData(_activeLocation);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeLocation);

            if (_activeLocation.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activeLocation).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    IsEditing();
                }
            }
            else
            {
                _activeLocation.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activeLocation.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Locations.Add(_activeLocation);
                    dbContext.SaveChanges();
                }

                OnAddToolBarItem();
            }

            MessageBoxService.SaveConfirmation(_activeLocation.Title);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeLocation.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var locationInDb = dbContext.Locations.Find(_activeLocation.Id);
                    dbContext.Locations.Remove(locationInDb);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeLocation = new Location();
                IsNotEditingAndAdding();
            }
        }

        protected override void OnAdding()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(UserControlTitle);
        }

        protected override void OnEditing()
        {
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeLocation.Title);
        }

        protected override void OnNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInactiveHeaderTitle(UserControlTitle);
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
            if (searchGridControl.ItemsSource is ObservableCollection<Location> locations)
                EditData(locations[rowIndex]);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var location = SearchGridControl.SelectedItem as Location;
            EditData(location);
        }

        public void EditData(Location location)
        {
            _activeLocation = location;
            FillData(location);
            IsEditing();
        }

        public void FillData(Location location)
        {
            NameTextEdit.EditValue = location.Title;
            DescriptionTextEdit.EditValue = location.Description;
            LocationTypeComboBoxEdit.EditValue = location.LocationType;
            LocationGroupComboBoxEdit.EditValue = location.LocationGroupId;
            DefaultCustomerComboBoxEdit.EditValue = location.DefaultCustomerId;
            LocationNumberSpinEdit.EditValue = location.Number;
            AvailableForSaleCheckEdit.EditValue = location.AvailableForSale;
            PickableCheckEdit.EditValue = location.Pickable;
            ReceivableCheckEdit.EditValue = location.Receivable;
            IsActiveCheckEdit.EditValue = location.IsActive;
        }

        public void ReadData(Location location)
        {
            location.Title = NameTextEdit.Text;
            location.Description = DescriptionTextEdit.Text;
            location.LocationType = (LocationType)LocationTypeComboBoxEdit.EditValue;
            location.LocationGroupId = Convert.ToInt32(LocationGroupComboBoxEdit.EditValue);
            location.DefaultCustomerId = (int?)DefaultCustomerComboBoxEdit.EditValue;
            location.Number = Convert.ToInt32(LocationNumberSpinEdit.EditValue);
            location.AvailableForSale = Convert.ToBoolean(AvailableForSaleCheckEdit.EditValue);
            location.Pickable = Convert.ToBoolean(PickableCheckEdit.EditValue);
            location.Receivable = Convert.ToBoolean(ReceivableCheckEdit.EditValue);
            location.IsActive = Convert.ToBoolean(IsActiveCheckEdit.EditValue);
        }

        private void GenerateNumberButtonOnClick(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var maxNumber = dbContext.Locations.Max(item => (int?)item.Number) ?? 0;
                LocationNumberSpinEdit.EditValue = maxNumber + 1;
            }
        }
    }
}
