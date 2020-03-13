using FarnahadManufacturing.Base;

// CHECK
namespace FarnahadManufacturing.Model.Configuration
{
    public class PartDefaultLocation : FmModelBase
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
        /// گروه موقعیت
        /// </summary>
        private int _locationGroupId;
        public int LocationGroupId
        {
            get => _locationGroupId;
            set
            {
                _locationGroupId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// گروه موقعیت
        /// </summary>
        private LocationGroup _locationGroup;
        public LocationGroup LocationGroup
        {
            get => _locationGroup;
            set
            {
                _locationGroup = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// موقعیت پیش فرض
        /// </summary>
        private int? _defaultLocationId;
        public int? DefaultLocationId
        {
            get => _defaultLocationId;
            set
            {
                _defaultLocationId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// موقعیت پیش فرض
        /// </summary>
        private Location _defaultLocation;
        public Location DefaultLocation
        {
            get => _defaultLocation;
            set
            {
                _defaultLocation = value;
                OnPropertyChanged();
            }
        }
    }
}
