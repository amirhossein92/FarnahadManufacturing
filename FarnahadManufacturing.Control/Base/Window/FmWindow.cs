using System.Windows.Media;
using DevExpress.Xpf.Core;

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
        }
    }
}
