using System;
using System.Collections.Generic;
using FarnahadManufacturing.Base;

namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// نرخ مالیات
    /// </summary>
    public class TaxRate : FmModelBase
    {
        public TaxRate()
        {
            Customers = new List<Customer>();
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
        /// مخفف
        /// </summary>
        private string _abbreviation;
        public string Abbreviation
        {
            get => _abbreviation;
            set
            {
                _abbreviation = value;
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
        /// براساس درصد
        /// </summary>
        private bool _isPercentageSelected;
        public bool IsPercentageSelected
        {
            get => _isPercentageSelected;
            set
            {
                _isPercentageSelected = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// درصد
        /// </summary>
        private double? _percentage;
        public double? Percentage
        {
            get => _percentage;
            set
            {
                _percentage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// نرخ ثابت
        /// </summary>
        private double? _flatRate;
        public double? FlatRate
        {
            get => _flatRate;
            set
            {
                _flatRate = value;
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
