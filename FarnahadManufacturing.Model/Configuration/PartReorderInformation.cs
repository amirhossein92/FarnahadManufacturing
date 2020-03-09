using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;

// CHECK
namespace FarnahadManufacturing.Model.Configuration
{
    public class PartReorderInformation : FmModelBase
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
        /// حداکثر مقدار کالا
        /// </summary>
        private double _orderUpToLevel;
        public double OrderUpToLevel
        {
            get => _orderUpToLevel;
            set
            {
                _orderUpToLevel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// حداقل مقدار کالا
        /// </summary>
        private double _reorderPoint;
        public double ReorderPoint
        {
            get => _reorderPoint;
            set
            {
                _reorderPoint = value;
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
