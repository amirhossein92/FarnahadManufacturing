using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using FarnahadManufacturing.UI.Base.Layout;
using FarnahadManufacturing.UI.Base.UserControl;
using FarnahadManufacturing.UI.Base.Window;
using UserControl = System.Windows.Controls.UserControl;

namespace FarnahadManufacturing.UI.Common
{
    public class WindowService
    {
        public static void OpenUserControlDialog<T>(T userControl) where T : DialogUserControlBase
        {
            var newWindow = new FmDialogWindow();
            newWindow.Title = userControl.UserControlTitle;
            var grid = new FmGrid();
            grid.Children.Add(userControl);
            newWindow.Content = grid;
            newWindow.ShowDialog();
        }
    }
}
