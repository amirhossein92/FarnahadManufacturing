using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Editors.Settings;

namespace FarnahadManufacturing.Control.Base.GridControl.Columns
{
    public class FmSpinEditGridColumn : FmGridColumn
    {
        public FmSpinEditGridColumn()
        {
            EditSettings = new SpinEditSettings();
        }
    }
}
