using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Model.BaseConfiguration
{
    public class Tracking : FmModelBase
    {
        // 4 Default Tracking Types =>
        // LOT Numbers : text
        // Revision Level : text
        // Expiration Date : datetime
        // Serial Number : serial number

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
