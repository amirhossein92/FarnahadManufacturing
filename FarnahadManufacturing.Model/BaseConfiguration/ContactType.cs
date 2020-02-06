using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Model.BaseConfiguration
{
    public class ContactType : FmModelBase
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

        private List<ContactInformation> _contactInformations;
        public List<ContactInformation> ContactInformations
        {
            get => _contactInformations;
            set
            {
                _contactInformations = value;
                OnPropertyChanged();
            }
        }
    }
}
