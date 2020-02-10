using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Bars;

namespace FarnahadManufacturing.UI.Common
{
    public static class ActiveToolBarService
    {
        private static Dictionary<string, IBarItem> ActiveToolBarItems { get; set; } = new Dictionary<string, IBarItem>();

        public static void AddBarItem(string key, IBarItem barItem)
        {
            ActiveToolBarItems.Add(key, barItem);
            SubscribeOnActiveToolBarChange?.Invoke(ActiveToolBarItems, ActiveToolBarEventArg.Add);
        }

        public static void AddBarItems(Dictionary<string, IBarItem> barItems)
        {
            foreach (var barItem in barItems)
                ActiveToolBarItems.Add(barItem.Key, barItem.Value);
            SubscribeOnActiveToolBarChange?.Invoke(ActiveToolBarItems, ActiveToolBarEventArg.Add);
        }

        public static IBarItem GetBarItem(string key)
        {
            if (ActiveToolBarItems.ContainsKey(key))
                return ActiveToolBarItems[key];

            return null;
        }

        public static void UpdateBarItem(string key, IBarItem barItem)
        {
            if (ActiveToolBarItems.ContainsKey(key))
            {
                ActiveToolBarItems[key] = barItem;
                SubscribeOnActiveToolBarChange?.Invoke(ActiveToolBarItems, ActiveToolBarEventArg.Update);
            }
        }

        public static void DeleteBarItem(string key)
        {
            if (ActiveToolBarItems.ContainsKey(key))
            {
                ActiveToolBarItems.Remove(key);
                SubscribeOnActiveToolBarChange?.Invoke(ActiveToolBarItems, ActiveToolBarEventArg.Delete);
            }
        }

        public static void ChangeToolBarItemStatus(string key, bool isEnable)
        {
            if (ActiveToolBarService.GetBarItem(key) is BarButtonItem toolBarItem)
            {
                toolBarItem.IsEnabled = isEnable;
                ActiveToolBarService.UpdateBarItem(key, toolBarItem);
            }
        }

        public delegate void ActiveEventChange(object sender, ActiveToolBarEventArg e);
        public static event ActiveEventChange SubscribeOnActiveToolBarChange;
    }

    public enum ActiveToolBarEventArg
    {
        Add,
        Update,
        Delete
    }
}
