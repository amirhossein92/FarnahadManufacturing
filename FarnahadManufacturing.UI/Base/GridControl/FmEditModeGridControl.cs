using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Grid;

namespace FarnahadManufacturing.UI.Base.GridControl
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
