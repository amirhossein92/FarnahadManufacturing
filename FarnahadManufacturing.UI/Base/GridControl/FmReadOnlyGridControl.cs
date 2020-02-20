using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Grid;

namespace FarnahadManufacturing.UI.Base.GridControl
{
    public class FmReadOnlyGridControl : DevExpress.Xpf.Grid.GridControl
    {
        public FmReadOnlyGridControl()
        {
            SelectionMode = MultiSelectMode.Row;
            View = new TableView
            {
                ShowSearchPanelMode = ShowSearchPanelMode.Always,
                AllowEditing = false,
            };
        }
    }
}
