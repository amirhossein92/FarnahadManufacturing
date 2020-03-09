using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

// CHECK
namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// آدرس
    /// </summary>
    public class Address : FmModelBase
    {
        public Address()
        {
            ContactInformations = new List<ContactInformation>();
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
        /// آدرس
        /// </summary>
        private string _addressDetail;
        public string AddressDetail
        {
            get => _addressDetail;
            set
            {
                _addressDetail = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// نوع آدرس
        /// </summary>
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

        /// <summary>
        /// نوع آدرس
        /// </summary>
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

        /// <summary>
        /// کشور
        /// </summary>
        private int? _countryId;
        public int? CountryId
        {
            get => _countryId;
            set
            {
                _countryId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// کشور
        /// </summary>
        private Country _country;
        public Country Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// شهر
        /// </summary>
        private int? _cityId;
        public int? CityId
        {
            get => _cityId;
            set
            {
                _cityId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// شهر
        /// </summary>
        private City _city;
        public City City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// استان
        /// </summary>
        private int? _provinceId;
        public int? ProvinceId
        {
            get => _provinceId;
            set
            {
                _provinceId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// استان
        /// </summary>
        private Province _province;
        public Province Province
        {
            get => _province;
            set
            {
                _province = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// کدپستی
        /// </summary>
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

        /// <summary>
        /// آدرس مسکونی
        /// </summary>
        private bool _isResidentialAddress;
        public bool IsResidentialAddress
        {
            get => _isResidentialAddress;
            set
            {
                _isResidentialAddress = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
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

        /// <summary>
        /// طول جغرافیایی
        /// </summary>
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

        private int _companyId;
        public int CompanyId
        {
            get => _companyId;
            set
            {
                _companyId = value;
                OnPropertyChanged();
            }
        }

        private Company _company;
        public Company Company
        {
            get => _company;
            set
            {
                _company = value;
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
