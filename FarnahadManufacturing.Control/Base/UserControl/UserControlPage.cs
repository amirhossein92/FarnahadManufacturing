using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.Bars;

// CHECK
namespace FarnahadManufacturing.Control.Base.UserControl
{
    public abstract class UserControlPage : UserControlBase
    {
        public UserControlPage()
        {
            FlowDirection = FlowDirection.RightToLeft;
            ToolBarItems = new Dictionary<string, IBarItem>();
        }

        public Dictionary<string, IBarItem> ToolBarItems { get; set; }
        public string ImagePath { get; set; }

        protected abstract void SetToolBarItems();
        protected abstract void InitialData();
    }
}
