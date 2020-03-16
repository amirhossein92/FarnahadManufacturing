using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using FarnahadManufacturing.Control.Common;

// CHECK
namespace FarnahadManufacturing.Control.Base.Window
{
    public class FmWindow : DXWindow
    {
        public FmWindow()
        {
            FontFamily = new FontFamily("Segoe UI");
            FontSize = 13;
            MinWidth = 600;
            MinHeight = 400;

            Closing += OnClosing;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (!MessageBoxService.AskForClose())
                e.Cancel = true;
        }
    }
}
