using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// کالا
    /// </summary>
    public class Part : FmModelBase
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
        /// شماره کالا
        /// </summary>
        private string _number;
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
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
        /// (Universal Product Code) کد جهانی کالا
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
        /// نوع کالا
        /// </summary>
        private PartType _partType;
        public PartType PartType
        {
            get => _partType;
            set
            {
                _partType = value;
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
        /// ردیابی کالا
        /// </summary>
        private List<TrackingPart> _trackingParts;
        public List<TrackingPart> TrackingParts
        {
            get => _trackingParts;
            set
            {
                _trackingParts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// محل پیش فرض
        /// </summary>
        private int? _defaultLocationId;
        public int? DefaultLocationId
        {
            get => _defaultLocationId;
            set
            {
                _defaultLocationId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// محل پیش فرض
        /// </summary>
        private Location _defaultLocation;
        public Location DefaultLocation
        {
            get => _defaultLocation;
            set
            {
                _defaultLocation = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// فروشنده پیش فرض
        /// </summary>
        private int? _defaultVendorId;
        public int? DefaultVendorId
        {
            get => _defaultVendorId;
            set
            {
                _defaultVendorId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// فروشنده پیش فرض
        /// </summary>
        private Vendor _defaultVendor;
        public Vendor DefaultVendor
        {
            get => _defaultVendor;
            set
            {
                _defaultVendor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// قابل استفاده در واحد اندازه گیری دیگر
        /// </summary>
        private bool _pickInPartUomOnly;
        public bool PickInPartUomOnly
        {
            get => _pickInPartUomOnly;
            set
            {
                _pickInPartUomOnly = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// جزئیات
        /// </summary>
        private string _details;
        public string Details
        {
            get => _details;
            set
            {
                _details = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// عکس کالا
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

        // TODO: change it to partCost list
        /// <summary>
        /// هزینه
        /// </summary>
        private double? _cost;
        public double? Cost
        {
            get => _cost;
            set
            {
                _cost = value;
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

        private List<Product> _products;
        public List<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
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
