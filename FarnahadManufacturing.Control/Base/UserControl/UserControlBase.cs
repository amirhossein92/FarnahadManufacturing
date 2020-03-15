using System.Windows;

namespace FarnahadManufacturing.Control.Base.UserControl
{
    public class UserControlBase : System.Windows.Controls.UserControl
    {
        public UserControlBase()
        {
            FlowDirection = FlowDirection.RightToLeft;
        }

        public string UserControlTitle { get; set; }
    }
}
