using System;
using System.Collections.Generic;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

// CHECK
namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// شرکت
    /// </summary>
    public class Company : FmModelBase
    {
        public Company()
        {
            Addresses = new List<Address>();
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
        /// لوگو شرکت
        /// </summary>
        private byte[] _logo;
        public byte[] Logo
        {
            get => _logo;
            set
            {
                _logo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// پیک پیش فرض
        /// </summary>
        private int? _defaultCarrierId;
        public int? DefaultCarrierId
        {
            get => _defaultCarrierId;
            set
            {
                _defaultCarrierId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// پیک پیش فرض
        /// </summary>
        private Carrier _defaultCarrier;
        public Carrier DefaultCarrier
        {
            get => _defaultCarrier;
            set
            {
                _defaultCarrier = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// خدمت پیش فرض پیک
        /// </summary>
        private int? _defaultCarrierServiceId;
        public int? DefaultCarrierServiceId
        {
            get => _defaultCarrierServiceId;
            set
            {
                _defaultCarrierServiceId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// خدمت پیش فرض پیک
        /// </summary>
        private CarrierService _defaultCarrierService;
        public CarrierService DefaultCarrierService
        {
            get => _defaultCarrierService;
            set
            {
                _defaultCarrierService = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// شرایط تحویل کالای پیش فرض
        /// </summary>
        private int? _defaultShippingTermId;
        public int? DefaultShippingTermId
        {
            get => _defaultShippingTermId;
            set
            {
                _defaultShippingTermId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// شرایط تحویل کالای پیش فرض
        /// </summary>
        private ShippingTerm _defaultShippingTerm;
        public ShippingTerm DefaultShippingTerm
        {
            get => _defaultShippingTerm;
            set
            {
                _defaultShippingTerm = value;
                OnPropertyChanged();
            }
        }

        private List<Address> _address;
        public List<Address> Addresses
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
