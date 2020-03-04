using System;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.UI.Common;

namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    public partial class UcAddressType : FilterUserControlBase
    {
        private ObservableCollection<AddressType> _addressTypes;
        private AddressType _activeAddressType;

        public UcAddressType()
        {
            InitializeComponent();

            UserControlTitle = "نوع آدرس";
            InitialData();
        }

        protected sealed override void InitialData()
        {
            LoadSearchGridControl();
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchTitleTextEdit.Text);
        }

        private void LoadSearchGridControlData(string title)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var addressTypesQueryable = dbContext.AddressTypes.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    addressTypesQueryable = addressTypesQueryable.Where(item => item.Title.Contains(title));
                var totalRecordsCount = addressTypesQueryable.Count();
                _addressTypes = addressTypesQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _addressTypes;
                PaginationUserControl.UpdateRecordsDetail(_addressTypes.Count, totalRecordsCount);
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activeAddressType = new AddressType();
            FillData(_activeAddressType);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeAddressType);
            if (_activeAddressType.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activeAddressType).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            else
            {
                _activeAddressType.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activeAddressType.CreatedDateTime = ApplicationSessionService.GetNowDateTime();

                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.AddressTypes.Add(_activeAddressType);
                    dbContext.SaveChanges();
                }
            }

            MessageBoxService.SaveConfirmation(_activeAddressType.Title);
            LoadSearchGridControl();
            IsEditing();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeAddressType.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var addressType = dbContext.AddressTypes.Find(_activeAddressType.Id);
                    dbContext.AddressTypes.Remove(addressType);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeAddressType = new AddressType();
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
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeAddressType.Title);
        }

        protected override void OnNotEditingAndAdding()
        {
            MainLayoutGroup.IsEnabled = false;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateInactiveHeaderTitle(UserControlTitle);
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void SearchGridControlOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var searchGridControl = (GridControl)sender;
            var rowIndex = searchGridControl.View.GetRowHandleByMouseEventArgs(e);
            if (rowIndex == DataControlBase.InvalidRowHandle)
                return;
            if (searchGridControl.ItemsSource is ObservableCollection<AddressType> addressTypes)
                EditData(addressTypes[rowIndex]);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var addressType = SearchGridControl.SelectedItem as AddressType;
            EditData(addressType);
        }

        private void EditData(AddressType addressType)
        {
            _activeAddressType = addressType;
            FillData(_activeAddressType);
            IsEditing();
        }

        private void FillData(AddressType addressType)
        {
            NameTextEdit.Text = addressType.Title;
            DescriptionTextEdit.Text = addressType.Description;
        }

        private void ReadData(AddressType addressType)
        {
            addressType.Title = NameTextEdit.Text;
            addressType.Description = DescriptionTextEdit.Text;
        }
    }
}
