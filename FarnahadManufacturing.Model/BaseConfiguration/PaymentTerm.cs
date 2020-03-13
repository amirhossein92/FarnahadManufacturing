using System;
using System.Collections.Generic;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Model.BaseConfiguration
{
    /// <summary>
    /// شرایط پرداخت
    /// </summary>
    public class PaymentTerm : FmModelBase
    {
        public PaymentTerm()
        {
            Customers = new List<Customer>();
            Vendors = new List<Vendor>();
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
        /// توضیحات
        /// </summary>
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

        /// <summary>
        /// نوع شرط پرداخت
        /// </summary>
        private bool _isNet;
        public bool IsNet
        {
            get => _isNet;
            set
            {
                _isNet = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// موعد پرداخت بعد از
        /// </summary>
        private int? _netNetDays;
        public int? NetNetDays
        {
            get => _netNetDays;
            set
            {
                _netNetDays = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// درصد تخفیف پرداخت پیش از موعد
        /// </summary>
        private double? _netDiscountPercentage;
        public double? NetDiscountPercentage
        {
            get => _netDiscountPercentage;
            set
            {
                _netDiscountPercentage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// تخفیف در صورت پرداخت پیش از
        /// </summary>
        private double? _netDiscountDays;
        public double? NetDiscountDays
        {
            get => _netDiscountDays;
            set
            {
                _netDiscountDays = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// تاریخ موعد پرداخت
        /// </summary>
        private int? _dateDrivenDueDate;
        public int? DateDrivenDueDate
        {
            get => _dateDrivenDueDate;
            set
            {
                _dateDrivenDueDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// پرداخت در ماه بعد در صورت خرید پس از
        /// </summary>
        private int? _dateDrivenNextMonthIfWithin;
        public int? DateDrivenNextMonthIfWithin
        {
            get => _dateDrivenNextMonthIfWithin;
            set
            {
                _dateDrivenNextMonthIfWithin = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// درصد تخفیف پرداخت پیش از موعد
        /// </summary>
        private double? _dateDrivenDiscountPercentage;
        public double? DateDrivenDiscountPercentage
        {
            get => _dateDrivenDiscountPercentage;
            set
            {
                _dateDrivenDiscountPercentage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// تخفیف در صورت پرداخت پیش از
        /// </summary>
        private int? _dateDrivenDiscountDate;
        public int? DateDrivenDiscountDate
        {
            get => _dateDrivenDiscountDate;
            set
            {
                _dateDrivenDiscountDate = value;
                OnPropertyChanged();
            }
        }

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

        /// <summary>
        /// غیر قابل تغییر
        /// </summary>
        private bool _readOnly;
        public bool ReadOnly
        {
            get => _readOnly;
            set
            {
                _readOnly = value;
                OnPropertyChanged();
            }
        }

        private List<Customer> _customers;
        public List<Customer> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }

        private List<Vendor> _vendors;
        public List<Vendor> Vendors
        {
            get => _vendors;
            set
            {
                _vendors = value;
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
