using System.Windows;
using DevExpress.Xpf.Editors.Settings;

// CHECK
namespace FarnahadManufacturing.Control.Base.GridControl.Columns
{
    public class FmCheckBoxGridColumn : FmGridColumn
    {
        public FmCheckBoxGridColumn()
        {
            EditSettings = new CheckEditSettings
            {
                FlowDirection = FlowDirection.RightToLeft,
            };
        }

    }
}
