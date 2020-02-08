using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
    public class LocationGroup : FmModelBase
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

        private int? _categoryId;
        public int? CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged();
            }
        }

        private Category _category;
        public Category Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        private List<User> _users;
        public List<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

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

        private List<Location> _locations;
        public List<Location> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                OnPropertyChanged();
            }
        }
    }
}
