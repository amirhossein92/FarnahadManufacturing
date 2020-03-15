using DevExpress.Xpf.Editors.Settings;

// CHECK
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
