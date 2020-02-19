using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;

namespace FarnahadManufacturing.UI.Base.GridControl.Columns
{
    public class FmCheckBoxGridColumn : FmGridColumn
    {
        public FmCheckBoxGridColumn()
        {
            EditSettings = new CheckEditSettings();
        }

    }
}
