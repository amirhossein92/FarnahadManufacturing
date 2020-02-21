using System.Windows.Controls;
using DevExpress.Xpf.LayoutControl;

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
