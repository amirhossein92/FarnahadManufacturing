using System;
using System.Collections.Generic;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Model.BaseConfiguration
{
    /// <summary>
    /// ردیابی
    /// </summary>
    public class Tracking : FmModelBase
    {
        // 4 Default Tracking Types =>
        // LOT Numbers : text
        // Revision Level : text
        // Expiration Date : datetime
        // Serial Number : serial number
        public Tracking()
        {
            TrackingParts = new List<TrackingPart>();
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
        /// نوع ردیابی
        /// </summary>
        private TrackingValueType _trackingValueType;
        public TrackingValueType TrackingValueType
        {
            get => _trackingValueType;
            set
            {
                _trackingValueType = value;
                OnPropertyChanged();
            }
        }

        private List<TrackingPart> _trackingParts;
        public List<TrackingPart> TrackingParts
        {
            get => _trackingParts;
            set
            {
                _trackingParts = value;
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
