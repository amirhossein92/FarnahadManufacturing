using System.Windows;

// CHECK
namespace FarnahadManufacturing.Control.Base.UserControl
{
    public class DialogUserControlBase : System.Windows.Controls.UserControl
    {
        public DialogUserControlBase()
        {
            FlowDirection = FlowDirection.RightToLeft;
        }

        public string UserControlTitle { get; set; }
    }
}
