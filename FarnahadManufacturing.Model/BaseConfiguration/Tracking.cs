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
        // Move this to baseConfigurations and the following default data
        // 4 Default Tracking Types =>
        // LOT Numbers : text
        // Revision Level : text
        // Expiration Date : datetime
        // Serial Number : serial number
        // Types: 
        
        // Another Class contains only the next value, part id and tracking id
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

        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        // TODO: Complete tracking...
    }
}
