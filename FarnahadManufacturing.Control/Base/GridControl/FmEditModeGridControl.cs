using DevExpress.Xpf.Grid;

namespace FarnahadManufacturing.Control.Base.GridControl
{
    public class FmEditModeGridControl : DevExpress.Xpf.Grid.GridControl
    {
        public FmEditModeGridControl()
        {
            SelectionMode = MultiSelectMode.Row;
            View = new TableView
            {
                ShowSearchPanelMode = ShowSearchPanelMode.Never,
                ShowGroupPanel = false,
                AllowEditing = true,
            };
        }
    }
}
