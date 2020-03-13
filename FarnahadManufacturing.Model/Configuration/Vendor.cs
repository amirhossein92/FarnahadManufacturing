using System.Collections.Generic;
using FarnahadManufacturing.Model.BaseConfiguration;

// CHECK
namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// فروشنده
    /// </summary>
    public class Vendor : Company
    {
        public Vendor()
        {
            Parts = new List<Part>();
        }

        /// <summary>
        /// شماره حساب
        /// </summary>
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

        /// <summary>
        /// حداقل میزان سفارش
        /// </summary>
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

        /// <summary>
        /// شرایط پرداخت پیش فرض
        /// </summary>
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

        /// <summary>
        /// شرایط پرداخت پیش فرض
        /// </summary>
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
    }
}
