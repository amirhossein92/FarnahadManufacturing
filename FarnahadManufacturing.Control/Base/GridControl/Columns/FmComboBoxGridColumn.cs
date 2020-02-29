using DevExpress.Xpf.Editors;

namespace FarnahadManufacturing.Control.Base.GridControl.Columns
{
    public class FmComboBoxGridColumn : FmGridColumn
    {
        public FmComboBoxGridColumn()
        {
            EditSettings = new FmComboBoxEditSettings { AllowNullInput = true, NullValueButtonPlacement = EditorPlacement.EditBox };
        }
    }
}
