using DevExpress.Xpf.Editors.Settings;

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
