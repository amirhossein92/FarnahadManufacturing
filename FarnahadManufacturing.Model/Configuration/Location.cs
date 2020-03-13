using System;
using System.Collections.Generic;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

// CHECK
namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// محل
    /// </summary>
    public class Location : FmModelBase
    {
        public Location()
        {
            Parts = new List<Part>();
            PartDefaultLocations = new List<PartDefaultLocation>();
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
        /// شماره
        /// </summary>
        private int _number;
        public int Number
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
        /// نوع محل
        /// </summary>
        private LocationType _locationType;
        public LocationType LocationType
        {
            get => _locationType;
            set
            {
                _locationType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// گروه محل
        /// </summary>
        private int _locationGroupId;
        public int LocationGroupId
        {
            get => _locationGroupId;
            set
            {
                _locationGroupId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// گروه محل
        /// </summary>
        private LocationGroup _locationGroup;
        public LocationGroup LocationGroup
        {
            get => _locationGroup;
            set
            {
                _locationGroup = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// مشتری پیش فرض
        /// </summary>
        private int? _defaultCustomerId;
        public int? DefaultCustomerId
        {
            get => _defaultCustomerId;
            set
            {
                _defaultCustomerId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// مشتری پیش فرض
        /// </summary>
        private Customer _defaultCustomer;
        public Customer DefaultCustomer
        {
            get => _defaultCustomer;
            set
            {
                _defaultCustomer = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// در دسترس برای فروش 
        /// </summary>
        private bool _availableForSale;
        public bool AvailableForSale
        {
            get => _availableForSale;
            set
            {
                _availableForSale = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// در دسترس برای گرفتن کالا
        /// </summary>
        private bool _pickable;
        public bool Pickable
        {
            get => _pickable;
            set
            {
                _pickable = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// در دسترس برای دریافت کالا
        /// </summary>
        private bool _receivable;
        public bool Receivable
        {
            get => _receivable;
            set
            {
                _receivable = value;
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

        private List<Part> _parts;
        public List<Part> Parts
        {
            get => _parts;
            set
            {
                _parts = value;
                OnPropertyChanged();
            }
        }

        private List<PartDefaultLocation> _partDefaultLocations;
        public List<PartDefaultLocation> PartDefaultLocations
        {
            get => _partDefaultLocations;
            set
            {
                _partDefaultLocations = value;
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
