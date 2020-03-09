using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Editors.Settings;

// CHECK
namespace FarnahadManufacturing.Control.Base.GridControl.Columns
{
    public class FmCurrencySpinEditGridColumn : FmGridColumn
    {
        public FmCurrencySpinEditGridColumn()
        {
            EditSettings = new SpinEditSettings
            {
                Mask = "###,###,###,###,##0.00 ریال"
            };
        }
    }
}
