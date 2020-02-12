using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using FlowDirection = System.Windows.FlowDirection;
using UserControl = System.Windows.Controls.UserControl;

namespace FarnahadManufacturing.UI.Common
{
    public class WindowService
    {
        public static void OpenUserControlDialog<T>(T userControl) where T : UserControl
        {
            var newWindow = new Window();
            var grid = new Grid();
            grid.FlowDirection = FlowDirection.RightToLeft;
            grid.Children.Add(userControl);
            newWindow.Content = grid;
            newWindow.ShowDialog();
        }
    }
}
