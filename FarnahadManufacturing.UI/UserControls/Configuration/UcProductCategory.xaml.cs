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
using DevExpress.Xpf.Utils;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcProductCategory : DialogUserControlBase
    {
        private ObservableCollection<ProductCategory> _productCategories;
        private ProductCategory _activeProductCategory;

        public UcProductCategory()
        {
            InitializeComponent();
            UserControlTitle = "دسته بندی محصولات";

            InitialData();
        }

        private void InitialData()
        {
            LoadProductCategories();
            LoadProductCategoriesTreeList();
        }

        private void LoadProductCategories()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var productCategories = dbContext.ProductCategories.Include(item => item.Products).ToList();
                _productCategories = new ObservableCollection<ProductCategory>(productCategories);
            }
        }

        private void LoadProductCategoriesTreeList()
        {
            foreach (var productCategory in _productCategories.Where(item => item.ParentProductCategoryId == null))
                ProductCategoriesTreeListView.Nodes.Add(GetChildrenProductCategory(productCategory));
        }

        private TreeListNode GetChildrenProductCategory(ProductCategory parentProductCategory)
        {
            var treeList = new TreeListNode(parentProductCategory);
            var children = _productCategories.Where(item => item.ParentProductCategoryId == parentProductCategory.Id);
            foreach (var child in children)
            {
                treeList.Nodes.Add(GetChildrenProductCategory(child));
            }

            return treeList;
        }

        private void OnAddClickButton(object sender, RoutedEventArgs e)
        {
        }

        private void OnEditClickButton(object sender, RoutedEventArgs e)
        {
        }

        private void OnDeleteClickButton(object sender, RoutedEventArgs e)
        {
        }

        private void OnRefreshClickButton(object sender, RoutedEventArgs e)
        {
        }

        private void AddProductButtonOnClick(object sender, RoutedEventArgs e)
        {
        }

        private void RemoveProductButtonOnClick(object sender, RoutedEventArgs e)
        {
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
        }

        private void SearchAdvancedButtonOnClick(object sender, RoutedEventArgs e)
        {
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
        }

        private void DisplayProductImageOnChecked(object sender, RoutedEventArgs e)
        {
        }

        private void CloseButtonOnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
