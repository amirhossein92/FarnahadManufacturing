using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
    public class User : FmModelBase
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

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        private string _initial;
        public string Initial
        {
            get => _initial;
            set
            {
                _initial = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private string _passwordSalt;
        public string PasswordSalt
        {
            get => _passwordSalt;
            set
            {
                _passwordSalt = value;
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

        private List<LocationGroup> _locationGroupMembers;
        public List<LocationGroup> LocationGroupMembers
        {
            get => _locationGroupMembers;
            set
            {
                _locationGroupMembers = value;
                OnPropertyChanged();
            }
        }

        private List<Customer> _customerSalespersons;
        public List<Customer> CustomerSalespersons
        {
            get => _customerSalespersons;
            set
            {
                _customerSalespersons = value;
                OnPropertyChanged();
            }
        }

        private List<AddressType> _addressTypes;
        public List<AddressType> AddressTypes
        {
            get => _addressTypes;
            set
            {
                _addressTypes = value;
                OnPropertyChanged();
            }
        }

        private List<Category> _categories;
        public List<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        private List<City> _cities;
        public List<City> Cities
        {
            get => _cities;
            set
            {
                _cities = value;
                OnPropertyChanged();
            }
        }

        private List<Country> _countries;
        public List<Country> Countries
        {
            get => _countries;
            set
            {
                _countries = value;
                OnPropertyChanged();
            }
        }

        private List<FobType> _fobTypes;
        public List<FobType> FobTypes
        {
            get => _fobTypes;
            set
            {
                _fobTypes = value;
                OnPropertyChanged();
            }
        }

        private List<LocationType> _locationTypes;
        public List<LocationType> LocationTypes
        {
            get => _locationTypes;
            set
            {
                _locationTypes = value;
                OnPropertyChanged();
            }
        }

        private List<PaymentMethod> _paymentMethods;
        public List<PaymentMethod> PaymentMethods
        {
            get => _paymentMethods;
            set
            {
                _paymentMethods = value;
                OnPropertyChanged();
            }
        }

        private List<PaymentTerm> _paymentTerms;
        public List<PaymentTerm> PaymentTerms
        {
            get => _paymentTerms;
            set
            {
                _paymentTerms = value;
                OnPropertyChanged();
            }
        }

        private List<ShippingTerm> _shippingTerms;
        public List<ShippingTerm> ShippingTerms
        {
            get => _shippingTerms;
            set
            {
                _shippingTerms = value;
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

        private List<UomType> _uomTypes;
        public List<UomType> UomTypes
        {
            get => _uomTypes;
            set
            {
                _uomTypes = value;
                OnPropertyChanged();
            }
        }

        private List<Address> _addresses;
        public List<Address> Addresses
        {
            get => _addresses;
            set
            {
                _addresses = value;
                OnPropertyChanged();
            }
        }

        private List<Carrier> _carriers;
        public List<Carrier> Carriers
        {
            get => _carriers;
            set
            {
                _carriers = value;
                OnPropertyChanged();
            }
        }

        private List<CarrierService> _carrierServices;
        public List<CarrierService> CarrierServices
        {
            get => _carrierServices;
            set
            {
                _carrierServices = value;
                OnPropertyChanged();
            }
        }

        private List<Company> _companies;
        public List<Company> Companies
        {
            get => _companies;
            set
            {
                _companies = value;
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

        private List<CustomerGroup> _customerGroups;
        public List<CustomerGroup> CustomerGroups
        {
            get => _customerGroups;
            set
            {
                _customerGroups = value;
                OnPropertyChanged();
            }
        }

        private List<Location> _locations;
        public List<Location> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                OnPropertyChanged();
            }
        }

        private List<LocationGroup> _locationGroups;
        public List<LocationGroup> LocationGroups
        {
            get => _locationGroups;
            set
            {
                _locationGroups = value;
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

        private List<TaxRate> _taxRates;
        public List<TaxRate> TaxRates
        {
            get => _taxRates;
            set
            {
                _taxRates = value;
                OnPropertyChanged();
            }
        }

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

        private List<Uom> _uoms;
        public List<Uom> Uoms
        {
            get => _uoms;
            set
            {
                _uoms = value;
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
