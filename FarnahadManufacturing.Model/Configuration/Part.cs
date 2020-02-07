using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
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

        private List<Tracking> _trackings;
        public List<Tracking> Trackings
        {
            get => _trackings;
            set
            {
                _trackings = value;
                OnPropertyChanged();
            }
        }

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
    }
}
