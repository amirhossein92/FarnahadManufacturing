using System;
using System.Collections.Generic;
using System.Configuration;
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
using DevExpress.DirectX.Common.Direct2D;
using DevExpress.Xpo.DB;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Common;

namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcProductCategoryAddEdit : DialogUserControlBase
    {
        private ProductCategory _productCategory;

        public UcProductCategoryAddEdit()
        {
            InitializeComponent();
            UserControlTitle = "اضافه کردن دسته بندی محصولات";
            _productCategory = new ProductCategory();
            LoadProductCategories();
        }

        public UcProductCategoryAddEdit(int productCategoryId) : this()
        {
            UserControlTitle = "ویرایش دسته بندی محصولات";
            _productCategory = GetProductCategory(productCategoryId);
            var parentProductCategories = ParentProductCategoryComboBoxEdit.ItemsSource as List<FmComboModel<int>>;
            FillData(_productCategory);
            LoadProductCategories();
        }

        private ProductCategory GetProductCategory(int productCategoryId)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                return dbContext.ProductCategories.First(item => item.Id == productCategoryId);
            }
        }

        private void LoadProductCategories()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var productCategoriesQueryable = dbContext.ProductCategories.AsQueryable();
                if (_productCategory.Id > 0)
                    productCategoriesQueryable =
                        productCategoriesQueryable.Where(item => item.Id != _productCategory.Id);
                var productCategories = productCategoriesQueryable
                    .Select(item => new FmComboModel<int> { Title = item.Title, Value = item.Id })
                    .ToList();
                ParentProductCategoryComboBoxEdit.ItemsSource = productCategories;
            }
        }

        private void ClickOnSave(object sender, RoutedEventArgs e)
        {
            ReadData(_productCategory);
            if (_productCategory.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_productCategory).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            else
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    _productCategory.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                    _productCategory.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                    dbContext.ProductCategories.Add(_productCategory);
                    dbContext.SaveChanges();
                }
            }
            WindowService.CloseUserControlDialogWindow(this);
        }

        private void ClickOnCancel(object sender, RoutedEventArgs e)
        {
            WindowService.CloseUserControlDialogWindow(this);
        }

        private void FillData(ProductCategory productCategory)
        {
            GroupNameTextEdit.Text = productCategory.Title;
            ParentProductCategoryComboBoxEdit.EditValue = productCategory.ParentProductCategoryId;
            DescriptionTextEdit.Text = productCategory.Description;
        }

        private void ReadData(ProductCategory productCategory)
        {
            productCategory.Title = GroupNameTextEdit.Text;
            productCategory.ParentProductCategoryId = (int?)ParentProductCategoryComboBoxEdit.EditValue;
            productCategory.Description = DescriptionTextEdit.Text;
        }
    }
}
