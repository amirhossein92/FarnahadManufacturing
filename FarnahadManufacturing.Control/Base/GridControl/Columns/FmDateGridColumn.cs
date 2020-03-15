using DevExpress.Xpf.Editors.Settings;

// CHECK
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
