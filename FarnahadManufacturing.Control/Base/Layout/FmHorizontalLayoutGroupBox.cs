using System.Windows.Controls;
using DevExpress.Xpf.LayoutControl;

// CHECK
namespace FarnahadManufacturing.Control.Base.Layout
{
    public class FmHorizontalLayoutGroupBox : LayoutGroup
    {
        public FmHorizontalLayoutGroupBox()
        {
            Orientation = Orientation.Horizontal;
            View = LayoutGroupView.GroupBox;
        }
    }
}
