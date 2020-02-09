using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
    public class Location : FmModelBase
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

        // TODO: Can be auto-generated
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

        private int _locationTypeId;
        public int LocationTypeId
        {
            get => _locationTypeId;
            set
            {
                _locationTypeId = value;
                OnPropertyChanged();
            }
        }

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
