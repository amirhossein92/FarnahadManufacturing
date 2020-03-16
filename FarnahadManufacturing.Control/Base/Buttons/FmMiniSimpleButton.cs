using System.Windows;
using System.Windows.Media;

namespace FarnahadManufacturing.Control.Base.Buttons
{
    public class FmMiniSimpleButton : FmSimpleButton
    {
        public FmMiniSimpleButton()
        {
            BorderBrush = Brushes.Transparent;
            BorderThickness = new Thickness(0);
            MinWidth = 0;
            Width = 30;
        }
    }
}
