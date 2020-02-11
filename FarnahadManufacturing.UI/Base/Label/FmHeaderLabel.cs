using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FarnahadManufacturing.UI.Base.Label
{
    public class FmHeaderLabel : TextBlock
    {
        public FmHeaderLabel()
        {
            Name = "PageHeaderLabel";
            Margin = new Thickness(5);
            VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
