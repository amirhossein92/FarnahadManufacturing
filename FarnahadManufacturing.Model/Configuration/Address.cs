using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
    public class Address : FmModelBase
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

        private int _addressTypeId;
        public int AddressTypeId
        {
            get => _addressTypeId;
            set
            {
                _addressTypeId = value;
                OnPropertyChanged();
            }
        }

        private AddressType _addressType;
        public AddressType AddressType
        {
            get => _addressType;
            set
            {
                _addressType = value;
                OnPropertyChanged();
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        private string _state;
        public string State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        private string _zipCode;
        public string ZipCode
        {
            get => _zipCode;
            set
            {
                _zipCode = value;
                OnPropertyChanged();
            }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        private bool _residentialAddress;
        public bool ResidentialAddress
        {
            get => _residentialAddress;
            set
            {
                _residentialAddress = value;
                OnPropertyChanged();
            }
        }

        private bool _isDefaultAddress;
        public bool IsDefaultAddress
        {
            get => _isDefaultAddress;
            set
            {
                _isDefaultAddress = value;
                OnPropertyChanged();
            }
        }

        private double? _latitude;
        public double? Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }

        private double? _longitude;
        public double? Longitude
        {
            get => _longitude;
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }

        private List<ContactInformation> _contactInformations;
        public List<ContactInformation> ContactInformations
        {
            get => _contactInformations;
            set
            {
                _contactInformations = value;
                OnPropertyChanged();
            }
        }
    }
}
