using System;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Model.BaseConfiguration
{
    // TODO: I THINK THE NAME SHOULD BE CHANGED => It is only stated as FOB in North America (http://petalpolymer.com/fa/node/37)
    // https://en.wikipedia.org/wiki/FOB_(shipping)
    /// <summary>
    /// فوب: قیمت تا لحظه تحویل روی کشتی
    /// </summary>
    public class FobType : FmModelBase
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
