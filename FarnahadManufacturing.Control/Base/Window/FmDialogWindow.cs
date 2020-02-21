using System.Windows;
using DevExpress.Xpf.Core;

namespace FarnahadManufacturing.Control.Base.Window
{
    public class FmDialogWindow : DXWindow
    {
        public FmDialogWindow()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
