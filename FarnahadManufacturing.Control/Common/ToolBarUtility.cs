using System.Collections.Generic;
using DevExpress.Xpf.Bars;

namespace FarnahadManufacturing.Control.Common
{
    public static class ToolBarUtility
    {
        public static void ChangeToolBarItemStatus(this Dictionary<string, IBarItem> toolBarItems, string key, bool isEnable)
        {
            if (toolBarItems.ContainsKey(key) && toolBarItems[key] is BarButtonItem toolBarItem)
                toolBarItem.IsEnabled = isEnable;
        }
    }
}
