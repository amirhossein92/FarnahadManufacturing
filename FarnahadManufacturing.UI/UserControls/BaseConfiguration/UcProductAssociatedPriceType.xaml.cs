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
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.UI.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    public partial class UcProductAssociatedPriceType : FilterUserControlBase
    {
        private ObservableCollection<ProductAssociatedPriceType> _productAssociatedPriceTypes;
        private ProductAssociatedPriceType _activeProductAssociatedPriceType;

        public UcProductAssociatedPriceType()
        {
            InitializeComponent();

            UserControlTitle = "نوع هزینه مرتبط با محصول";
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
                var productAssociatedPriceTypesQueryable = dbContext.ProductAssociatedPriceTypes.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(title))
                    productAssociatedPriceTypesQueryable =
                        productAssociatedPriceTypesQueryable.Where(item => item.Title.Contains(title));
                var totalRecordsCount = productAssociatedPriceTypesQueryable.Count();
                _productAssociatedPriceTypes = productAssociatedPriceTypesQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _productAssociatedPriceTypes;
                PaginationUserControl.UpdateRecordsDetail(_productAssociatedPriceTypes.Count, totalRecordsCount);
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activeProductAssociatedPriceType = new ProductAssociatedPriceType();
            FillData(_activeProductAssociatedPriceType);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeProductAssociatedPriceType);
            if (_activeProductAssociatedPriceType.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_activeProductAssociatedPriceType).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }

                IsEditing();
            }
            else
            {
                _activeProductAssociatedPriceType.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                _activeProductAssociatedPriceType.CreatedDateTime = ApplicationSessionService.GetNowDateTime();

                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.ProductAssociatedPriceTypes.Add(_activeProductAssociatedPriceType);
                    dbContext.SaveChanges();
                }

                OnAddToolBarItem();
            }

            MessageBoxService.SaveConfirmation(_activeProductAssociatedPriceType.Title);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeProductAssociatedPriceType.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var productAssociatedPriceType = dbContext.ProductAssociatedPriceTypes.Find(_activeProductAssociatedPriceType.Id);
                    dbContext.ProductAssociatedPriceTypes.Remove(productAssociatedPriceType);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeProductAssociatedPriceType = new ProductAssociatedPriceType();
                IsNotEditingAndAdding();
            }
        }

        protected override void OnAdding()
        {
            NameTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(UserControlTitle);
        }

        protected override void OnEditing()
        {
            NameTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeProductAssociatedPriceType.Title);
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
            if (searchGridControl.ItemsSource is ObservableCollection<ProductAssociatedPriceType> productAssociatedPriceTypes)
                EditData(productAssociatedPriceTypes[rowIndex]);
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var productAssociatedPriceType = SearchGridControl.SelectedItem as ProductAssociatedPriceType;
            EditData(productAssociatedPriceType);
        }

        private void EditData(ProductAssociatedPriceType productAssociatedPriceType)
        {
            _activeProductAssociatedPriceType = productAssociatedPriceType;
            FillData(_activeProductAssociatedPriceType);
            IsEditing();
        }

        private void FillData(ProductAssociatedPriceType productAssociatedPriceType)
        {
            NameTextEdit.Text = productAssociatedPriceType.Title;
            IsTaxableCheckEdit.EditValue = productAssociatedPriceType.IsTaxable;
            DescriptionTextEdit.Text = productAssociatedPriceType.Description;
        }

        private void ReadData(ProductAssociatedPriceType productAssociatedPriceType)
        {
            productAssociatedPriceType.Title = NameTextEdit.Text;
            productAssociatedPriceType.Description = DescriptionTextEdit.Text;
            productAssociatedPriceType.IsTaxable = (bool)IsTaxableCheckEdit.EditValue;
        }
    }
}
