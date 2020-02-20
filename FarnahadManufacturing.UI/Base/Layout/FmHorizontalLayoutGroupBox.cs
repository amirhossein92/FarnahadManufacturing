using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using DevExpress.Xpf.LayoutControl;

namespace FarnahadManufacturing.UI.Base.Layout
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
