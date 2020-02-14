using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// مشتری
    /// </summary>
    public class Customer : Company
    {
        // TODO: Not sure about the inheritance => what if Customer is Person and not company!
        public Customer()
        {
            CustomerGroups = new List<CustomerGroup>();
            Locations = new List<Location>();
        }

        /// <summary>
        /// حد اعتبار
        /// </summary>
        private double? _creditLimit;
        public double? CreditLimit
        {
            get => _creditLimit;
            set
            {
                _creditLimit = value;
                OnPropertyChanged();
            }
        }

        // TODO: Status?
        // TODO: IssueStatus?
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

        /// <summary>
        /// فروشنده
        /// </summary>
        private int? _salespersonId;
        public int? SalespersonId
        {
            get => _salespersonId;
            set
            {
                _salespersonId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// فروشنده
        /// </summary>
        private User _salesperson;
        public User Salesperson
        {
            get => _salesperson;
            set
            {
                _salesperson = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// دسته بندی
        /// </summary>
        private int? _categoryId;
        public int? CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// دسته بندی
        /// </summary>
        private Category _category;
        public Category Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged();
            }
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

        // TODO: Default Priority?

        /// <summary>
        /// نرخ مالیاتی
        /// </summary>
        private int _taxRateId;
        public int TaxRateId
        {
            get => _taxRateId;
            set
            {
                _taxRateId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// نرخ مالیاتی
        /// </summary>
        private TaxRate _taxRate;
        public TaxRate TaxRate
        {
            get => _taxRate;
            set
            {
                _taxRate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// معاف از مالیات
        /// </summary>
        private bool _isTaxExempt;
        public bool IsTaxExempt
        {
            get => _isTaxExempt;
            set
            {
                _isTaxExempt = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// شماره معاف از مالیات
        /// </summary>
        private string _taxExemptNumber;
        public string TaxExemptNumber
        {
            get => _taxExemptNumber;
            set
            {
                _taxExemptNumber = value;
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
    }
}
