using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using DevExpress.Xpf.LayoutControl;

namespace FarnahadManufacturing.UI.Base.Layout
{
    public class FmLayoutItem : LayoutItem
    {
        public FmLayoutItem()
        {
            this.AddColonToLabel = true;
            LabelWidth = 100;
        }
    }
}
