using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcProductCategory : DialogUserControl
    {
        private ObservableCollection<ProductCategory> _productCategories;

        public UcProductCategory()
        {
            InitializeComponent();
            UserControlTitle = "دسته بندی محصولات";

            InitialData();
        }

        private void InitialData()
        {
            LoadProducts();
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
            ProductCategoriesTreeListView.Nodes.Clear();
            foreach (var productCategory in _productCategories.Where(item => item.ParentProductCategoryId == null))
                ProductCategoriesTreeListView.Nodes.Add(GetChildrenProductCategory(productCategory));
        }

        private TreeListNode GetChildrenProductCategory(ProductCategory parentProductCategory)
        {
            var treeList = new TreeListNode(parentProductCategory);
            var children = _productCategories.Where(item => item.ParentProductCategoryId == parentProductCategory.Id);
            foreach (var product in parentProductCategory.Products)
                treeList.Nodes.Add(new TreeListNode(product));
            foreach (var child in children)
                treeList.Nodes.Add(GetChildrenProductCategory(child));

            return treeList;
        }

        private void OnAddClickButton(object sender, RoutedEventArgs e)
        {
            WindowService.OpenUserControlDialog(new UcProductCategoryAddEdit());
        }

        private void OnEditClickButton(object sender, RoutedEventArgs e)
        {
            if (ProductCategoriesTreeListControl.SelectedItem is ProductCategory productCategory)
            {
                WindowService.OpenUserControlDialog(new UcProductCategoryAddEdit(productCategory.Id));
            }
            else
            {
                // No Valid Item Selected

            }
        }

        private void OnDeleteClickButton(object sender, RoutedEventArgs e)
        {
            if (ProductCategoriesTreeListControl.SelectedItem is ProductCategory productCategory)
            {
                if (MessageBoxService.AskForDelete(productCategory.Title) == true)
                {
                    using (var dbContext = new FarnahadManufacturingDbContext())
                    {
                        var productCategoryInDb = dbContext.ProductCategories.First(item => item.Id == productCategory.Id);
                        dbContext.ProductCategories.Remove(productCategoryInDb);
                        dbContext.SaveChanges();
                    }
                }
            }
            else
            {
                // No Valid Item Selected
            }
        }

        private void OnRefreshClickButton(object sender, RoutedEventArgs e)
        {
            LoadProductCategories();
            LoadProductCategoriesTreeList();
        }

        private void AddProductButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (ProductCategoriesTreeListControl.SelectedItem is ProductCategory productCategory &&
                SearchGridControl.SelectedItem is Product product)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var productCategoryInDb = dbContext.ProductCategories.First(item => item.Id == productCategory.Id);
                    var tempProducts = productCategory.Products;
                    tempProducts.Add(product);
                    productCategoryInDb.Products.Clear();
                    foreach (var tempProduct in tempProducts)
                    {
                        var productInDb = dbContext.Products.Find(tempProduct.Id);
                        productCategoryInDb.Products.Add(productInDb);
                    }
                    dbContext.SaveChanges();
                }
            }
            LoadProductCategories();
            LoadProductCategoriesTreeList();
        }

        private void RemoveProductButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (ProductCategoriesTreeListControl.SelectedItem is Product product)
            {
                var productNode = ProductCategoriesTreeListControl.GetSelectedNodes();
                var productCategory = productNode[0].ParentNode.Content as ProductCategory;
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    dbContext.Products.Attach(product);
                    var productCategoryInDb = dbContext.ProductCategories.First(item => item.Id == productCategory.Id);
                    productCategoryInDb.Products.Remove(product);
                    dbContext.SaveChanges();
                }
            }
            LoadProductCategories();
            LoadProductCategoriesTreeList();
        }

        private void SearchButtonOnClick(object sender, RoutedEventArgs e)
        {
            LoadProducts(ProductNumberTextEdit.Text, ProductDescriptionTextEdit.Text,
                CustomerNumberTextEdit.Text, PartNumberTextEdit.Text);
        }

        private void LoadProducts(string productName = null, string productDescription = null, string customerNumber = null, string partNumber = null)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var productsQueryable = dbContext.Products.OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(productName))
                    productsQueryable = productsQueryable.Where(item => item.Title.Contains(productName));
                if (!string.IsNullOrEmpty(productDescription))
                    productsQueryable = productsQueryable.Where(item => item.Description.Contains(productDescription));
                // TODO: add customer...
                //if (!string.IsNullOrEmpty(customerNumber))
                //    productsQueryable = productsQueryable.Where(item => item.c);
                if (!string.IsNullOrEmpty(partNumber))
                    productsQueryable = productsQueryable.Where(item => item.Part.Number.Contains(partNumber));
                var products = productsQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = products;
                PaginationUserControl.UpdateRecordsDetail(products.Count, productsQueryable.Count());
            }
        }

        private void SearchAdvancedButtonOnClick(object sender, RoutedEventArgs e)
        {
        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadProducts(ProductNumberTextEdit.Text, ProductDescriptionTextEdit.Text,
                CustomerNumberTextEdit.Text, PartNumberTextEdit.Text);
        }

        private void DisplayProductImageOnChecked(object sender, RoutedEventArgs e)
        {
            ProductImageEditLayoutItem.Visibility = Visibility.Visible;
        }

        private void DisplayProductImageOnUnchecked(object sender, RoutedEventArgs e)
        {
            ProductImageEditLayoutItem.Visibility = Visibility.Collapsed;
        }

        private void CloseButtonOnClick(object sender, RoutedEventArgs e)
        {
            WindowService.CloseUserControlDialogWindow(this);
        }
    }
}
