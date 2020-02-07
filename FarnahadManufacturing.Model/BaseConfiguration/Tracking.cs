using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;

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

        private TrackingType _trackingType;
        public TrackingType TrackingType
        {
            get => _trackingType;
            set
            {
                _trackingType = value;
                OnPropertyChanged();
            }
        }
    }
}
