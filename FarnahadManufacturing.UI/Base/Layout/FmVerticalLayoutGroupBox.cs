using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DevExpress.Xpf.LayoutControl;

namespace FarnahadManufacturing.UI.Base.Layout
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
