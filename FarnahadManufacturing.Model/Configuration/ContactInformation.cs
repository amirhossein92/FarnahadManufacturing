using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
    public class ContactInformation : FmModelBase
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

        private int _contactTypeId;
        public int ContactTypeId
        {
            get => _contactTypeId;
            set
            {
                _contactTypeId = value;
                OnPropertyChanged();
            }
        }

        private ContactType _contactType;
        public ContactType ContactType
        {
            get => _contactType;
            set
            {
                _contactType = value;
                OnPropertyChanged();
            }
        }

        private bool _isDefault;
        public bool IsDefault
        {
            get => _isDefault;
            set
            {
                _isDefault = value;
                OnPropertyChanged();
            }
        }

        private int _addressId;
        public int AddressId
        {
            get => _addressId;
            set
            {
                _addressId = value;
                OnPropertyChanged();
            }
        }

        private Address _address;
        public Address Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }
    }
}
