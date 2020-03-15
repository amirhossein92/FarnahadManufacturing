using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.UI.Common;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.BaseConfiguration
{
    public partial class UcCategoryAddEdit : DialogUserControlBase
    {
        private Category _category;

        public UcCategoryAddEdit()
        {
            InitializeComponent();

            UserControlTitle = "اضافه کردن تقسیم بندی";
            _category = new Category();
            LoadCategories();
        }

        public UcCategoryAddEdit(int categoryId) : this()
        {
            UserControlTitle = "ویرایش دسته بندی محصولات";
            _category = GetCategory(categoryId);
            var parentProductCategories = ParentCategoryComboBoxEdit.ItemsSource as List<FmComboModel<int>>;
            FillData(_category);
            LoadCategories();
        }

        private Category GetCategory(int categoryId)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                return dbContext.Categories.First(item => item.Id == categoryId);
            }
        }

        private void LoadCategories()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var categoriesQueryable = dbContext.Categories.AsQueryable();
                if (_category.Id > 0)
                    categoriesQueryable =
                        categoriesQueryable.Where(item => item.Id != _category.Id);
                var categories = categoriesQueryable
                    .Select(item => new FmComboModel<int> { Title = item.Title, Value = item.Id })
                    .ToList();
                ParentCategoryComboBoxEdit.ItemsSource = categories;
            }
        }

        private void ClickOnSave(object sender, RoutedEventArgs e)
        {
            ReadData(_category);
            if (_category.Id > 0)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Entry(_category).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            else
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    _category.CreatedByUserId = ApplicationSessionService.GetActiveUserId();
                    _category.CreatedDateTime = ApplicationSessionService.GetNowDateTime();
                    dbContext.Categories.Add(_category);
                    dbContext.SaveChanges();
                }
            }
            WindowService.CloseUserControlDialogWindow(this);
        }

        private void ClickOnCancel(object sender, RoutedEventArgs e)
        {
            WindowService.CloseUserControlDialogWindow(this);
        }

        private void FillData(Category category)
        {
            TitleTextEdit.Text = category.Title;
            ParentCategoryComboBoxEdit.EditValue = category.ParentCategoryId;
            DescriptionTextEdit.Text = category.Description;
        }

        private void ReadData(Category category)
        {
            category.Title = TitleTextEdit.Text;
            category.ParentCategoryId = (int?)ParentCategoryComboBoxEdit.EditValue;
            category.Description = DescriptionTextEdit.Text;
        }

    }
}
