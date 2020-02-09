using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Model.BaseConfiguration
{
    public class PaymentTerm : FmModelBase
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

        private bool _isDateDriven;
        public bool IsDateDriven
        {
            get => _isDateDriven;
            set
            {
                _isDateDriven = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _dateDrivenDueDate;
        public DateTime? DateDrivenDueDate
        {
            get => _dateDrivenDueDate;
            set
            {
                _dateDrivenDueDate = value;
                OnPropertyChanged();
            }
        }

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

        private DateTime? _dateDrivenDiscountDate;
        public DateTime? DateDrivenDiscountDate
        {
            get => _dateDrivenDiscountDate;
            set
            {
                _dateDrivenDiscountDate = value;
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
