using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.UI.Base.ViewModel
{
    public class FmComboModel<T>
    {
        public FmComboModel()
        {

        }

        public FmComboModel(T value, string title) : this()
        {
            Value = value;
            Title = title;
        }

        public T Value { get; set; }
        public string Title { get; set; }
    }
}
