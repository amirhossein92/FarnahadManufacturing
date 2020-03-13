using System;
using System.Collections.Generic;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

// CHECK
namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// محصول
    /// </summary>
    public class Product : FmModelBase
    {
        public Product()
        {
            ProductPrices = new List<ProductPrice>();
            ProductCategories = new List<ProductCategory>();
            ProductSubstitutes = new List<ProductSubstitute>();
            ProductProductSubstitutes = new List<ProductSubstitute>();
            ProductAssociatePrices = new List<ProductAssociatePrice>();
        }

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// عنوان
        /// </summary>
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// کالا
        /// </summary>
        private int _partId;
        public int PartId
        {
            get => _partId;
            set
            {
                _partId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// کالا
        /// </summary>
        private Part _part;
        public Part Part
        {
            get => _part;
            set
            {
                _part = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// توضیحات
        /// </summary>
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// جزئیات محصول
        /// </summary>
        private string _detail;
        public string Detail
        {
            get => _detail;
            set
            {
                _detail = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// واحد اندازه گیری
        /// </summary>
        private int _uomId;
        public int UomId
        {
            get => _uomId;
            set
            {
                _uomId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// واحد اندازه گیری
        /// </summary>
        private Uom _uom;
        public Uom Uom
        {
            get => _uom;
            set
            {
                _uom = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// قابل فروش در واحد اندازه گیری دیگر
        /// </summary>
        private bool _allowToSellInOtherUom;
        public bool AllowToSellInOtherUom
        {
            get => _allowToSellInOtherUom;
            set
            {
                _allowToSellInOtherUom = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// دسته بندی
        /// </summary>
        private int? _categoryId;
        public int? CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// دسته بندی
        /// </summary>
        private Category _category;
        public Category Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// عکس محصول
        /// </summary>
        private byte[] _picture;
        public byte[] Picture
        {
            get => _picture;
            set
            {
                _picture = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// مشمول مالیات
        /// </summary>
        private bool _isTaxable;
        public bool IsTaxable
        {
            get => _isTaxable;
            set
            {
                _isTaxable = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// نمایش در سفارش خرید
        /// </summary>
        private bool _showOnSaleOrder;
        public bool ShowOnSaleOrder
        {
            get => _showOnSaleOrder;
            set
            {
                _showOnSaleOrder = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// کد جهانی محصول (Universal Product Code)
        /// </summary>
        private string _upc;
        public string Upc
        {
            get => _upc;
            set
            {
                _upc = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Stop Keeping Unit
        /// </summary>
        private string _sku;
        public string Sku
        {
            get => _sku;
            set
            {
                _sku = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// یادداشت اخطار
        /// </summary>
        private string _alertNote;
        public string AlertNote
        {
            get => _alertNote;
            set
            {
                _alertNote = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// نوع محصول در فاکتور فروش
        /// </summary>
        private SaleOrderItemType _saleOrderItemType;
        public SaleOrderItemType SaleOrderItemType
        {
            get => _saleOrderItemType;
            set
            {
                _saleOrderItemType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// طول
        /// </summary>
        private double? _length;
        public double? Length
        {
            get => _length;
            set
            {
                _length = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// عرض
        /// </summary>
        private double? _width;
        public double? Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// ارتفاع
        /// </summary>
        private double? _height;
        public double? Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// واحد اندازه گیری فاصله
        /// </summary>
        private int _distanceUomId;
        public int DistanceUomId
        {
            get => _distanceUomId;
            set
            {
                _distanceUomId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// واحد اندازه گیری فاصله
        /// </summary>
        private Uom _distanceUom;
        public Uom DistanceUom
        {
            get => _distanceUom;
            set
            {
                _distanceUom = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// وزن
        /// </summary>
        private double? _weight;
        public double? Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// واحد اندازه گیری وزن
        /// </summary>
        private int _weightUomId;
        public int WeightUomId
        {
            get => _weightUomId;
            set
            {
                _weightUomId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// واحد اندازه گیری وزن
        /// </summary>
        private Uom _weightUom;
        public Uom WeightUom
        {
            get => _weightUom;
            set
            {
                _weightUom = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// فعال
        /// </summary>
        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }

        private List<ProductPrice> _productPrices;
        public List<ProductPrice> ProductPrices
        {
            get => _productPrices;
            set
            {
                _productPrices = value;
                OnPropertyChanged();
            }
        }

        private List<ProductCategory> _productCategories;
        public List<ProductCategory> ProductCategories
        {
            get => _productCategories;
            set
            {
                _productCategories = value;
                OnPropertyChanged();
            }
        }

        private List<ProductSubstitute> _productSubstitutes;
        public List<ProductSubstitute> ProductSubstitutes
        {
            get => _productSubstitutes;
            set
            {
                _productSubstitutes = value;
                OnPropertyChanged();
            }
        }

        private List<ProductSubstitute> _productProductSubstitutes;
        public List<ProductSubstitute> ProductProductSubstitutes
        {
            get => _productProductSubstitutes;
            set
            {
                _productProductSubstitutes = value;
                OnPropertyChanged();
            }
        }

        private List<ProductAssociatePrice> _productAssociatePrices;
        public List<ProductAssociatePrice> ProductAssociatePrices
        {
            get => _productAssociatePrices;
            set
            {
                _productAssociatePrices = value;
                OnPropertyChanged();
            }
        }

        private int _createdByUserId;
        public int CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                _createdByUserId = value;
                OnPropertyChanged();
            }
        }

        private User _createdByUser;
        public User CreatedByUser
        {
            get => _createdByUser;
            set
            {
                _createdByUser = value;
                OnPropertyChanged();
            }
        }

        private DateTime _createdDateTime;
        public DateTime CreatedDateTime
        {
            get => _createdDateTime;
            set
            {
                _createdDateTime = value;
                OnPropertyChanged();
            }
        }
    }
}
