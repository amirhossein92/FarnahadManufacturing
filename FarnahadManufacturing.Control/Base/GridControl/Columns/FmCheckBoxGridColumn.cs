using DevExpress.Xpf.Editors.Settings;

namespace FarnahadManufacturing.Control.Base.GridControl.Columns
{
    public class FmCheckBoxGridColumn : FmGridColumn
    {
        public FmCheckBoxGridColumn()
        {
            EditSettings = new CheckEditSettings();
        }

    }
}
