using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Docking;

namespace FarnahadManufacturing.Control.Base.Layout
{
    public class FmDockLayoutPanel : LayoutPanel
    {
        public FmDockLayoutPanel()
        {
            AllowDrag = false;
            AllowMove = false;
        }
    }
}
