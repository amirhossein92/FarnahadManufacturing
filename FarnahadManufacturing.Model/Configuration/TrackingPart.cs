using System;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

// CHECK
namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// ردیابی کالا
    /// </summary>
    public class TrackingPart : FmModelBase
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
        /// ردیابی
        /// </summary>
        private int _trackingId;
        public int TrackingId
        {
            get => _trackingId;
            set
            {
                _trackingId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// ردیابی
        /// </summary>
        private Tracking _tracking;
        public Tracking Tracking
        {
            get => _tracking;
            set
            {
                _tracking = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// کالا
        /// </summary>
        private int _partId;
        public int PartId
        {
            get => _partId;
            set
            {
                _partId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// کالا
        /// </summary>
        private Part _part;
        public Part Part
        {
            get => _part;
            set
            {
                _part = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// مقدار بعدی
        /// </summary>
        private string _nextValue;
        public string NextValue
        {
            get => _nextValue;
            set
            {
                _nextValue = value;
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
        /// اصلی
        /// </summary>
        private bool _isPrimary;
        public bool IsPrimary
        {
            get => _isPrimary;
            set
            {
                _isPrimary = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// انتخاب شده
        /// </summary>
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
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