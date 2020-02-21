using System.Windows;
using System.Windows.Controls;

namespace FarnahadManufacturing.Control.Base.Label
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
