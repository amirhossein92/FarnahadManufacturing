using DevExpress.Xpf.Grid;

// CHECK
namespace FarnahadManufacturing.Control.Base.GridControl
{
    public class FmReadOnlyGridControl : DevExpress.Xpf.Grid.GridControl
    {
        public FmReadOnlyGridControl()
        {
            SelectionMode = MultiSelectMode.Row;
            View = new TableView
            {
                ShowSearchPanelMode = ShowSearchPanelMode.Never,
                ShowGroupPanel = false,
                AllowEditing = false,
            };
        }
    }
}
