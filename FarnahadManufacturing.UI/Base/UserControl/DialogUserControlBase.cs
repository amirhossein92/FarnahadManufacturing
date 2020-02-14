using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FarnahadManufacturing.UI.Base.UserControl
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
