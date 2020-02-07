using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
    public class Company : FmModelBase
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
    }
}
