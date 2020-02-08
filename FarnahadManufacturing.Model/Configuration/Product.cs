using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
    public class Product : FmModelBase
    {
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

        // TODO: SO ITEM TYPE

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
    }
}
