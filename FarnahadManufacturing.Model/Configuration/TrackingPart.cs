using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
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
    }
}