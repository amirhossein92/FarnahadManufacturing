using DevExpress.Xpf.Editors.Settings;

namespace FarnahadManufacturing.Control.Base.GridControl.Columns
{
    public class FmDateGridColumn : FmTextGridColumn
    {
        public FmDateGridColumn()
        {
            EditSettings = new DateEditSettings();
        }
    }
}
