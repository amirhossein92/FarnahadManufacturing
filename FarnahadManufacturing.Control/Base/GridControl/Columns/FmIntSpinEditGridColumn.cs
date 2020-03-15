using DevExpress.Xpf.Editors.Settings;

// CHECK
namespace FarnahadManufacturing.Control.Base.GridControl.Columns
{
    public class FmIntSpinEditGridColumn : FmSpinEditGridColumn
    {
        public FmIntSpinEditGridColumn()
        {
            EditSettings = new SpinEditSettings{Mask = "N0"};
        }
    }
}
