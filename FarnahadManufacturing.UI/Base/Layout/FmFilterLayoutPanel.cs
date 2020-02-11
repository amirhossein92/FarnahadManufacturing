using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Docking;

namespace FarnahadManufacturing.UI.Base.Layout
{
    public class FmFilterLayoutPanel : LayoutPanel
    {
        public FmFilterLayoutPanel()
        {
            Caption = "فیلتر";
            AllowDrag = false;
            AllowMove = false;
        }
    }
}
