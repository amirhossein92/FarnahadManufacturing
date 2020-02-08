using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
    public class Vendor : Company
    {
        private string _accountNumber;
        public string AccountNumber
        {
            get => _accountNumber;
            set
            {
                _accountNumber = value;
                OnPropertyChanged();
            }
        }

        private double? _minOrderAmount;
        public double? MinOrderAmount
        {
            get => _minOrderAmount;
            set
            {
                _minOrderAmount = value;
                OnPropertyChanged();
            }
        }

        private int? _defaultPaymentTermId;
        public int? DefaultPaymentTermId
        {
            get => _defaultPaymentTermId;
            set
            {
                _defaultPaymentTermId = value;
                OnPropertyChanged();
            }
        }

        private PaymentTerm _defaultPaymentTerm;
        public PaymentTerm DefaultPaymentTerm
        {
            get => _defaultPaymentTerm;
            set
            {
                _defaultPaymentTerm = value;
                OnPropertyChanged();
            }
        }

        // TODO: What is status?

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
    }
}
