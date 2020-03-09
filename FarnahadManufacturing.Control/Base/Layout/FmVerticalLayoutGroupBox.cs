using System.Windows.Controls;
using DevExpress.Xpf.LayoutControl;

// CHECK
namespace FarnahadManufacturing.Control.Base.Layout
{
    public class FmVerticalLayoutGroupBox : LayoutGroup
    {
        public FmVerticalLayoutGroupBox()
        {
            Orientation = Orientation.Vertical;
            View = LayoutGroupView.GroupBox;
        }
    }
}
