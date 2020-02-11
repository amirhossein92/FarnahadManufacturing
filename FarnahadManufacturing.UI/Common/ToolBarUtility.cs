using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Bars;

namespace FarnahadManufacturing.UI.Common
{
    public static class ToolBarUtility
    {

        public static void ChangeToolBarItemStatus(Dictionary<string, IBarItem> toolBarItems, string key, bool isEnable)
        {
            if (toolBarItems.ContainsKey(key) && toolBarItems[key] is BarButtonItem toolBarItem)
                toolBarItem.IsEnabled = isEnable;
        }

    }
}
