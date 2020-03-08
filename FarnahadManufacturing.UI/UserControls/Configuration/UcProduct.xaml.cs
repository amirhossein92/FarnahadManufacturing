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
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.Base.Common;
using FarnahadManufacturing.Control.Base.UserControl;
using FarnahadManufacturing.Control.Base.ViewModel;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;
using FarnahadManufacturing.UI.Common;

namespace FarnahadManufacturing.UI.UserControls.Configuration
{
    public partial class UcProduct : FilterUserControlBase
    {
        private ObservableCollection<Product> _products;
        private Product _activeProduct;

        public UcProduct()
        {
            InitializeComponent();

            UserControlTitle = "محصولات";
            AddCustomToolBarItems();
            InitialData();
        }

        private void AddCustomToolBarItems()
        {
            var productCategoryButtonItem = new BarButtonItem
            {
                Name = "ProductCategory",
                Content = "دسته بندی",
                Glyph = ImageUtility.CreateSvgImage("Icons/NavigationBar/ProductCategory_Large.svg"),
            };
            productCategoryButtonItem.ItemClick += ProductCategoryButtonItemOnItemClick;
            ToolBarItems.Add("ProductCategory", productCategoryButtonItem);
        }

        private void ProductCategoryButtonItemOnItemClick(object sender, ItemClickEventArgs e)
        {
            WindowService.OpenUserControlDialog(new UcProductCategory());
        }

        protected sealed override void InitialData()
        {
            LoadSearchGridControl();

            LoadParts();
            LoadUoms();
            LoadDistanceUoms();
            LoadWeightUoms();
            LoadCategories();
            LoadSaleOrders();
            LoadProductSubstitute();
            LoadProductAssociatePriceTypes();
        }

        private void LoadParts()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var parts = dbContext.Parts.AsNoTracking().Select(item => new FmComboModel<int>
                {
                    Title = item.Title,
                    Value = item.Id,
                }).ToList();
                PartComboBoxEdit.ItemsSource = parts;
            }
        }

        private void LoadUoms()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var uoms = dbContext.Uoms.AsNoTracking().ToList();
                UomComboBoxEdit.ItemsSource = uoms;
            }
        }

        private void LoadDistanceUoms()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var uoms = dbContext.Uoms.AsNoTracking().Where(item => item.UomType == UomType.Length).ToList();
                DistanceUomComboBoxEdit.ItemsSource = uoms;
            }
        }

        private void LoadWeightUoms()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var uoms = dbContext.Uoms.AsNoTracking().Where(item => item.UomType == UomType.Weight).ToList();
                WeightUomComboBoxEdit.ItemsSource = uoms;
            }
        }

        private void LoadCategories()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var categories = dbContext.Categories.AsNoTracking()
                    .Select(item => new FmComboModel<int>
                    {
                        Title = item.Title,
                        Value = item.Id,
                    }).ToList();
                CategoryComboBoxEdit.ItemsSource = categories;
            }
        }

        private void LoadSaleOrders()
        {
            // TODO: Rename ENUMS
            var saleOrderItemTypes =
                new List<FmComboModel<SaleOrderItemType>>
                {
                    new FmComboModel<SaleOrderItemType>(SaleOrderItemType.Sale, "فروش"),
                    new FmComboModel<SaleOrderItemType>(SaleOrderItemType.CreditReturn, "بازگشت هزینه"),
                    new FmComboModel<SaleOrderItemType>(SaleOrderItemType.DropShip, "دراپ شیپ"),
                };
            SaleOrderItemTypeComboBoxEdit.ItemsSource = saleOrderItemTypes;
        }

        private void LoadProductSubstitute()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var products = dbContext.Products.AsNoTracking()
                    .Select(item => new FmComboModel<int>
                    {
                        Title = item.Title,
                        Value = item.Id,
                    }).ToList();
                ProductSubstituteComboBoxColumn.ItemsSource = products;
            }
        }

        private void LoadProductAssociatePriceTypes()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var productAssociatePriceTypes = dbContext.ProductAssociatedPriceTypes.AsNoTracking()
                    .Select(item => new FmComboModel<int>
                    {
                        Title = item.Title,
                        Value = item.Id,
                    }).ToList();
                ProductAssociatePriceTypeComboBoxGridColumn.ItemsSource = productAssociatePriceTypes;
            }
        }

        public override void LoadSearchGridControl()
        {
            LoadSearchGridControlData(SearchProductNumberTextEdit.Text, SearchProductDescriptionTextEdit.Text,
                SearchPartNumberTextEdit.Text, SearchCustomerNumberTextEdit.Text);
        }

        private void LoadSearchGridControlData(string productTitle, string productDescription, string partNumber, string customerNumber)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var productsQueryable = dbContext.Products
                    .Include(item => item.ProductPrices)
                    .Include(item => item.ProductSubstitutes)
                    .Include(item => item.ProductAssociatePrices)
                    .OrderBy(item => item.Id).AsQueryable();
                if (!string.IsNullOrEmpty(productTitle))
                    productsQueryable = productsQueryable.Where(item => item.Title.Contains(productTitle));
                if (!string.IsNullOrEmpty(productDescription))
                    productsQueryable = productsQueryable.Where(item => item.Description.Contains(productDescription));
                if (!string.IsNullOrEmpty(partNumber))
                    productsQueryable = productsQueryable.Where(item => item.Part.Number.Contains(partNumber));
                // TODO: Customer Number
                //if (!string.IsNullOrEmpty(customerNumber))
                //    productsQueryable = productsQueryable.Where(item => item.Customer.Number.Contains(customerNumber));
                var totalRecordsCount = productsQueryable.Count();
                _products = productsQueryable.Paginate(PaginationUserControl.CurrentPage);
                SearchGridControl.ItemsSource = _products;
                PaginationUserControl.UpdateRecordsDetail(_products.Count, totalRecordsCount);
            }
        }

        protected override void OnAddToolBarItem()
        {
            _activeProduct = new Product();
            FillData(_activeProduct);
            IsAdding();
        }

        protected override void OnSaveToolBarItem()
        {
            ReadData(_activeProduct);
            if (_activeProduct.Id > 0)
            {
                var activeUserId = ApplicationSessionService.GetActiveUserId();
                var creationDate = ApplicationSessionService.GetNowDateTime();

                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var productInDb = dbContext.Products
                        .Include(item => item.ProductPrices)
                        .Include(item => item.ProductSubstitutes)
                        .Include(item => item.ProductAssociatePrices)
                        .First(item => item.Id == _activeProduct.Id);
                    var newPrice = _activeProduct.ProductPrices.FirstOrDefault(item => item.CreatedByUserId == 0);
                    if (newPrice != null)
                    {
                        newPrice.CreatedByUserId = activeUserId;
                        newPrice.CreatedDateTime = creationDate;
                        newPrice.ProductId = productInDb.Id;
                        productInDb.ProductPrices.Add(newPrice);
                    }

                    foreach (var productAssociatePrice in _activeProduct.ProductAssociatePrices)
                    {
                        if (productAssociatePrice.ProductId == 0)
                        {
                            productAssociatePrice.ProductId = _activeProduct.Id;
                            productAssociatePrice.CreatedByUserId = activeUserId;
                            productAssociatePrice.CreatedDateTime = creationDate;
                        }
                        else
                        {
                            var productAssociatePriceInDb = dbContext.ProductAssociatePrices.Find(productAssociatePrice.Id);
                            productAssociatePriceInDb.ProductAssociatedPriceTypeId = productAssociatePrice.ProductAssociatedPriceTypeId;
                            productAssociatePriceInDb.Price = productAssociatePrice.Price;
                        }
                    }
                    productInDb.ProductAssociatePrices = _activeProduct.ProductAssociatePrices;

                    //foreach (var productSubstitute in _activeProduct.ProductSubstitutes)
                    //{
                    //    if (productSubstitute.ProductId == 0)
                    //    {
                    //        productSubstitute.ProductId = _activeProduct.Id;
                    //        productSubstitute.CreatedByUserId = activeUserId;
                    //        productSubstitute.CreatedDateTime = creationDate;
                    //    }
                    //    else
                    //    {
                    //        //var productSubstituteInDb = dbContext.ProductSubstitutes.Find(productSubstitute.Id);
                    //        //productSubstituteInDb.Note = productSubstitute.Note;
                    //        //productSubstituteInDb.SubstituteProductId = productSubstitute.SubstituteProductId;
                    //        //var a = dbContext.Entry(productSubstitute).State;
                    //        //dbContext.ProductSubstitutes.Attach(productSubstitute);
                    //        //var b = dbContext.Entry(productSubstitute).State;

                    //    }
                    //}
                    //productInDb.ProductSubstitutes = _activeProduct.ProductSubstitutes;

                    productInDb.Title = _activeProduct.Title;
                    productInDb.PartId = _activeProduct.PartId;
                    productInDb.UomId = _activeProduct.UomId;
                    productInDb.Description = _activeProduct.Description;
                    productInDb.Detail = _activeProduct.Detail;
                    productInDb.IsActive = _activeProduct.IsActive;
                    productInDb.AllowToSellInOtherUom = _activeProduct.AllowToSellInOtherUom;
                    productInDb.CategoryId = _activeProduct.CategoryId;
                    productInDb.IsTaxable = _activeProduct.IsTaxable;
                    productInDb.ShowOnSaleOrder = _activeProduct.ShowOnSaleOrder;
                    productInDb.Picture = _activeProduct.Picture;

                    productInDb.Sku = _activeProduct.Sku;
                    productInDb.Upc = _activeProduct.Upc;
                    productInDb.AlertNote = _activeProduct.AlertNote;
                    productInDb.SaleOrderItemType = _activeProduct.SaleOrderItemType;
                    productInDb.Length = _activeProduct.Length;
                    productInDb.Width = _activeProduct.Width;
                    productInDb.Height = _activeProduct.Height;
                    productInDb.DistanceUomId = _activeProduct.DistanceUomId;
                    productInDb.Weight = _activeProduct.Weight;
                    productInDb.WeightUomId = _activeProduct.WeightUomId;

                    dbContext.SaveChanges();

                    IsEditing();
                }
            }
            else
            {
                var activeUserId = ApplicationSessionService.GetActiveUserId();
                var createDate = ApplicationSessionService.GetNowDateTime();
                _activeProduct.CreatedByUserId = activeUserId;
                _activeProduct.CreatedDateTime = createDate;

                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    foreach (var productAssociatePrice in _activeProduct.ProductAssociatePrices)
                    {
                        productAssociatePrice.CreatedByUserId = activeUserId;
                        productAssociatePrice.CreatedDateTime = createDate;
                    }
                    foreach (var productPrice in _activeProduct.ProductPrices)
                    {
                        productPrice.CreatedByUserId = activeUserId;
                        productPrice.CreatedDateTime = createDate;
                    }
                    foreach (var productSubstitute in _activeProduct.ProductSubstitutes)
                    {
                        productSubstitute.CreatedByUserId = activeUserId;
                        productSubstitute.CreatedDateTime = createDate;
                    }

                    dbContext.Products.Add(_activeProduct);
                    dbContext.SaveChanges();

                    OnAddToolBarItem();
                }
            }

            MessageBoxService.SaveConfirmation(_activeProduct.Title);
            LoadSearchGridControl();
        }

        protected override void OnDeleteToolBarItem()
        {
            if (MessageBoxService.AskForDelete(_activeProduct.Title) == true)
            {
                using (var dbContext = new FarnahadManufacturingDbContext())
                {
                    var productInDb = dbContext.Products.FirstOrDefault(item => item.Id == _activeProduct.Id);
                    dbContext.Products.Remove(productInDb);
                    dbContext.SaveChanges();
                }

                LoadSearchGridControl();
                _activeProduct = new Product();
                IsNotEditingAndAdding();
            }
        }

        protected override void OnAdding()
        {
            ProductTitleTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle = HeaderService.GenerateAddHeaderTitle(UserControlTitle);
        }

        protected override void OnEditing()
        {
            ProductTitleTextEdit.Focus();
            MainLayoutGroup.IsEnabled = true;
            FmHeaderLayoutGroup.HeaderTitle =
                HeaderService.GenerateEditHeaderTitle(UserControlTitle, _activeProduct.Title);
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
            if (searchGridControl.ItemsSource is ObservableCollection<Product> products)
                EditData(products[rowIndex]);
        }

        private void SearchAdvancedButtonOnClick(object sender, RoutedEventArgs e)
        {

        }

        private void PaginationUserControlOnRefreshData(object sender, RoutedEventArgs e)
        {
            LoadSearchGridControl();
        }

        private void DisplayProductImageOnChecked(object sender, RoutedEventArgs e)
        {
            ProductImageEditLayoutItem.Visibility = Visibility.Visible;
        }

        private void DisplayProductImageOnUnchecked(object sender, RoutedEventArgs e)
        {
            ProductImageEditLayoutItem.Visibility = Visibility.Collapsed;
        }

        private void ViewButtonOnClick(object sender, RoutedEventArgs e)
        {
            var product = SearchGridControl.SelectedItem as Product;
            EditData(product);
        }

        private void EditData(Product product)
        {
            _activeProduct = product;
            FillData(_activeProduct);
            IsEditing();
        }

        private void FillData(Product product)
        {
            ProductTitleTextEdit.Text = product.Title;
            ProductDescriptionTextEdit.Text = product.Description;
            PartComboBoxEdit.EditValue = product.PartId;
            PartDescriptionLabel.Text = GetPartDescription(product.PartId);
            PriceSpinEdit.EditValue = product.ProductPrices.OrderByDescending(item => item.CreatedDateTime).FirstOrDefault()?.Price;
            UomComboBoxEdit.EditValue = product.UomId;
            CategoryComboBoxEdit.EditValue = product.CategoryId;
            IsActiveCheckEdit.EditValue = product.IsActive;
            ShowOnSalesOrderCheckEdit.EditValue = product.ShowOnSaleOrder;
            IsTaxableCheckEdit.EditValue = product.IsTaxable;
            AllowToSellInOtherUomCheckEdit.EditValue = product.AllowToSellInOtherUom;
            DetailTextEdit.Text = product.Detail;
            PictureImageEdit.EditValue = product.Picture;

            UpcTextEdit.Text = product.Upc;
            SkuTextEdit.Text = product.Sku;
            AlertNoteTextEdit.Text = product.AlertNote;
            SaleOrderItemTypeComboBoxEdit.EditValue = product.SaleOrderItemType;
            LengthSpinEdit.EditValue = product.Length;
            WidthSpinEdit.EditValue = product.Width;
            HeightSpinEdit.EditValue = product.Height;
            DistanceUomComboBoxEdit.EditValue = product.DistanceUomId;
            WeightSpinEdit.EditValue = product.Weight;
            WeightUomComboBoxEdit.EditValue = product.WeightUomId;
            // Inventories

            ProductSubstituteGridControl.ItemsSource = GetProductSubstitutes(product.Id);
            // Product Associated Kits

            // Price Rules
            ProductAssociatePriceGridControl.ItemsSource = GetProductAssociates(product.Id);

            // Customers
        }

        private string GetPartDescription(int partId)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                return dbContext.Parts.FirstOrDefault(item => item.Id == partId)?.Description;
            }
        }

        private ObservableCollection<ProductAssociatePrice> GetProductAssociates(int productId)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var productAssociatePrices = dbContext.ProductAssociatePrices.AsNoTracking()
                    .Where(item => item.ProductId == productId).ToList();
                return new ObservableCollection<ProductAssociatePrice>(productAssociatePrices);
            }
        }

        private ObservableCollection<ProductSubstitute> GetProductSubstitutes(int productId)
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var productSubstitutes = dbContext.ProductSubstitutes.AsNoTracking()
                    .Include(item => item.Product)
                    .Where(item => item.ProductId == productId).ToList();
                return new ObservableCollection<ProductSubstitute>(productSubstitutes);
            }
        }

        private void ReadData(Product product)
        {
            product.Title = ProductTitleTextEdit.Text;
            product.Description = ProductDescriptionTextEdit.Text;
            product.PartId = (int)PartComboBoxEdit.EditValue;
            var lastPrice = product.ProductPrices
                .OrderByDescending(item => item.CreatedDateTime).FirstOrDefault()?.Price;
            if (lastPrice == null || (lastPrice != null && lastPrice != Convert.ToDouble(PriceSpinEdit.EditValue)))
                product.ProductPrices.Add(new ProductPrice { Price = Convert.ToDouble(PriceSpinEdit.EditValue) });
            product.UomId = (int)UomComboBoxEdit.EditValue;
            product.CategoryId = (int?)CategoryComboBoxEdit.EditValue;
            product.IsActive = (bool)IsActiveCheckEdit.EditValue;
            product.ShowOnSaleOrder = (bool)ShowOnSalesOrderCheckEdit.EditValue;
            product.IsTaxable = (bool)IsTaxableCheckEdit.EditValue;
            product.AllowToSellInOtherUom = (bool)AllowToSellInOtherUomCheckEdit.EditValue;
            product.Detail = DetailTextEdit.Text;
            product.Picture = (byte[])PictureImageEdit.EditValue;

            product.Upc = UpcTextEdit.Text;
            product.Sku = SkuTextEdit.Text;
            product.AlertNote = AlertNoteTextEdit.Text;
            product.SaleOrderItemType = (SaleOrderItemType)SaleOrderItemTypeComboBoxEdit.EditValue;
            product.Length = (double?)LengthSpinEdit.EditValue;
            product.Width = (double?)WidthSpinEdit.EditValue;
            product.Height = (double?)HeightSpinEdit.EditValue;
            product.DistanceUomId = (int)DistanceUomComboBoxEdit.EditValue;
            product.Weight = (double?)WeightSpinEdit.EditValue;
            product.WeightUomId = (int)WeightUomComboBoxEdit.EditValue;
            // Inventories

            if (ProductSubstituteGridControl.ItemsSource is ObservableCollection<ProductSubstitute> productSubstitutes)
                product.ProductSubstitutes = productSubstitutes.ToList();
            // Product Associated Kits

            // Price Rules
            if (ProductAssociatePriceGridControl.ItemsSource is ObservableCollection<ProductAssociatePrice> productAssociatePrices)
                product.ProductAssociatePrices = productAssociatePrices.ToList();

            // Customers
        }

        private void PartComboBoxEditOnEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            var partId = (int)e.NewValue;
            PartDescriptionLabel.Text = GetPartDescription(partId);
        }

        private void SearchGridControlOnSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.NewItem is Product selectedProduct)
                SearchProductImageEdit.EditValue = selectedProduct.Picture;
        }

        private void ProductSubstitutesListUserControlOnClickOnAddItem(object sender, RoutedEventArgs e)
        {
            var productSubstitute = new ProductSubstitute();
            WindowService.OpenUserControlDialog(new UcProductSubstitute(productSubstitute));
            var dataIsAdded = ApplicationDataStore.GetData<bool>("IsAddedOrChanged");
            if (dataIsAdded)
            {
                _activeProduct.ProductSubstitutes.Add(productSubstitute);
                ProductSubstituteGridControl.ItemsSource =
                    new ObservableCollection<ProductSubstitute>(_activeProduct.ProductSubstitutes);
            }
        }

        private void ProductSubstitutesListUserControlOnClickOnDeleteItem(object sender, RoutedEventArgs e)
        {
            if (ProductSubstituteGridControl.SelectedItem is ProductSubstitute productSubstitute)
            {
                if (MessageBoxService.AskForDelete() == true)
                {
                    _activeProduct.ProductSubstitutes.Remove(productSubstitute);
                    ProductSubstituteGridControl.ItemsSource =
                        new ObservableCollection<ProductSubstitute>(_activeProduct.ProductSubstitutes);
                }
            }
        }

        private void ProductSubstitutesListUserControlOnClickOnEditItem(object sender, RoutedEventArgs e)
        {
            if (ProductSubstituteGridControl.SelectedItem is ProductSubstitute productSubstitute)
            {
                WindowService.OpenUserControlDialog(new UcProductSubstitute(productSubstitute));
                var dataIsChanged = ApplicationDataStore.GetData<bool>("IsAddedOrChanged");
                if (dataIsChanged)
                {
                    ProductSubstituteGridControl.ItemsSource =
                        new ObservableCollection<ProductSubstitute>(_activeProduct.ProductSubstitutes);
                }
            }
        }

        private void ProductAssociatePriceListUserControlOnClickOnAddItem(object sender, RoutedEventArgs e)
        {
            var productAssociatePrice = new ProductAssociatePrice();
            WindowService.OpenUserControlDialog(new UcProductAssociatePrice(productAssociatePrice));
            var dataIsAdded = ApplicationDataStore.GetData<bool>("IsAddedOrChanged");
            if (dataIsAdded)
            {
                _activeProduct.ProductAssociatePrices.Add(productAssociatePrice);
                ProductAssociatePriceGridControl.ItemsSource =
                    new ObservableCollection<ProductAssociatePrice>(_activeProduct.ProductAssociatePrices);
            }
        }

        private void ProductAssociatePriceListUserControlOnClickOnEditItem(object sender, RoutedEventArgs e)
        {
            if (ProductAssociatePriceGridControl.SelectedItem is ProductAssociatePrice productAssociatePrice)
            {
                WindowService.OpenUserControlDialog(new UcProductAssociatePrice(productAssociatePrice));
                var dataIsChanged = ApplicationDataStore.GetData<bool>("IsAddedOrChanged");
                if (dataIsChanged)
                {
                    ProductAssociatePriceGridControl.ItemsSource =
                        new ObservableCollection<ProductAssociatePrice>(_activeProduct.ProductAssociatePrices);
                }
            }
        }

        private void ProductAssociatePriceListUserControlOnClickOnDeleteItem(object sender, RoutedEventArgs e)
        {
            if (ProductAssociatePriceGridControl.SelectedItem is ProductAssociatePrice productAssociatePrice)
            {
                if (MessageBoxService.AskForDelete() == true)
                {
                    _activeProduct.ProductAssociatePrices.Remove(productAssociatePrice);
                    ProductAssociatePriceGridControl.ItemsSource =
                        new ObservableCollection<ProductAssociatePrice>(_activeProduct.ProductAssociatePrices);
                }
            }
        }
    }
}
