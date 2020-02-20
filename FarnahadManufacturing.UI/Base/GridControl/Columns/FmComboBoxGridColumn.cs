using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Editors.Settings;

namespace FarnahadManufacturing.UI.Base.GridControl.Columns
{
    public class FmComboBoxGridColumn : FmGridColumn
    {
        public FmComboBoxGridColumn()
        {
            EditSettings = new FmComboBoxEditSettings();
        }
    }
}
