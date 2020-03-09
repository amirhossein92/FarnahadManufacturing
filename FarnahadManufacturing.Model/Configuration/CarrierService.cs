using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;

// CHECK
namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// خدمات پیک
    /// </summary>
    public class CarrierService : FmModelBase
    {
        public CarrierService()
        {
            Companies = new List<Company>();
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
        /// کد
        /// </summary>
        private string _code;
        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// پیک
        /// </summary>
        private int _carrierId;
        public int CarrierId
        {
            get => _carrierId;
            set
            {
                _carrierId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// پیک
        /// </summary>
        private Carrier _carrier;
        public Carrier Carrier
        {
            get => _carrier;
            set
            {
                _carrier = value;
                OnPropertyChanged();
            }
        }

        private List<Company> _companies;
        public List<Company> Companies
        {
            get => _companies;
            set
            {
                _companies = value;
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
