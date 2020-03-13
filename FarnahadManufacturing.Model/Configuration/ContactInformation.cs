using System;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// اطلاعات تماس
    /// </summary>
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
        /// مقدار
        /// </summary>
        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// نوع ارتباط
        /// </summary>
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

        /// <summary>
        /// پیش فرض
        /// </summary>
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

        /// <summary>
        /// آدرس
        /// </summary>
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
        
        /// <summary>
        /// آدرس
        /// </summary>
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
