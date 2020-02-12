﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.EditForm;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Base.UserControl;
using FarnahadManufacturing.UI.Common;
using Application = System.Windows.Application;

namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    /// <summary>
    /// Interaction logic for UcCarrier.xaml
    /// </summary>
    public partial class UcCarrier : UserControlBase
    {
        private ObservableCollection<Carrier> _carriers;
        private ObservableCollection<CarrierService> _carrierServices;
        private Carrier _activeCarrier;
        private int _currentPage = 1;
        private int _totalRecordsCount;
        private string _userControlTitle = "پیک";

        public UcCarrier()
        {
            InitializeComponent();

            SetToolBarItems();
            InitialData();
        }

        protected sealed override void SetToolBarItems()
        {
            var addButton = new BarButtonItem
            {
                Name = "Add",
                Content = "اضافه",
                Glyph = new BitmapImage(new Uri("Assets/AccordionIcons/Icon_32x32.png", UriKind.Relative)),
            };
            addButton.ItemClick += AddButtonOnToolBarItemClick;
            var saveButton = new BarButtonItem
            {
                Name = "Save",
                Content = "ذخیره",
                Glyph = new BitmapImage(new Uri("Assets/AccordionIcons/Icon_32x32.png", UriKind.Relative)),

            };
            saveButton.ItemClick += SaveButtonOnToolBarItemClick;
            var deleteButton = new BarButtonItem
            {
                Name = "Delete",
                Content = "حذف",
                Glyph = new BitmapImage(new Uri("Assets/AccordionIcons/Icon_32x32.png", UriKind.Relative)),

            };
            deleteButton.ItemClick += DeleteButtonOnToolBarItemClick;
            ToolBarItems.Add(addButton.Name, addButton);
            ToolBarItems.Add(saveButton.Name, saveButton);
            ToolBarItems.Add(deleteButton.Name, deleteButton);
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
            SearchStatusComboBoxEdit.DisplayMember = "Item2";
            SearchStatusComboBoxEdit.ValueMember = "Item1";
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
                _totalRecordsCount = carrierQueryable.Count();
                _carriers = carrierQueryable.Paginate(_currentPage);
                SearchGridControl.ItemsSource = _carriers;
                RecordsCountFmLabel.Text =
                    PaginationUtility.GetRecordsDetailText(_currentPage, _carriers.Count, _totalRecordsCount);
            }
        }

        private void AddButtonOnToolBarItemClick(object sender, ItemClickEventArgs e)
        {
            _activeCarrier = new Carrier();
            EditData(_activeCarrier);
            IsAdding();
        }

        private void SaveButtonOnToolBarItemClick(object sender, ItemClickEventArgs e)
        {
            ReadData(ref _activeCarrier);
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
                }
            }
            else
            {
                _activeCarrier.CreatedByUserId = 3;
                _activeCarrier.CreatedDateTime = DateTime.Now;
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    _activeCarrier.CarrierServices.AddRange(_carrierServices);
                    dbContext.Carriers.Add(_activeCarrier);
                    dbContext.SaveChanges();
                }
            }

            MessageBoxService.SaveConfirmation(_activeCarrier.Title);
            LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchStatusComboBoxEdit.SelectedItemValue);
            IsEditing();
        }

        private void DeleteButtonOnToolBarItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageBoxService.AskForDelete(_activeCarrier.Title) == DialogResult.Yes)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var carrier = dbContext.Carriers.Find(_activeCarrier.Id);
                    dbContext.Carriers.Remove(carrier);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchStatusComboBoxEdit.SelectedItemValue);
                _activeCarrier = new Carrier();
            }

            IsNotEditingAndAdding();
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchStatusComboBoxEdit.SelectedItemValue);
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

        private void PreviousPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchStatusComboBoxEdit.SelectedItemValue);
            }
        }

        private void NextPageButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (_currentPage <= (_totalRecordsCount / ApplicationSetting.PageRecordNumber))
            {
                _currentPage++;
                LoadSearchGridControlData(SearchTitleTextEdit.Text, SearchStatusComboBoxEdit.SelectedItemValue);
            }
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

        private void ReadData(ref Carrier carrier)
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
            if (MessageBoxService.AskForDelete(activeCarrierService.Title) == DialogResult.Yes)
                _carrierServices.Remove(activeCarrierService);
        }

        private void IsAdding()
        {
            MainLayoutGroup.IsEnabled = true;
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Add", true);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Save", true);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Delete", false);
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(_userControlTitle);
        }

        private void IsEditing()
        {
            MainLayoutGroup.IsEnabled = true;
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Add", true);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Save", true);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Delete", true);
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(_userControlTitle, _activeCarrier.Title);
        }

        private void IsNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Add", true);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Save", false);
            ToolBarUtility.ChangeToolBarItemStatus(ToolBarItems, "Delete", false);
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInActiveHeaderTitle(_userControlTitle);
        }
    }
}