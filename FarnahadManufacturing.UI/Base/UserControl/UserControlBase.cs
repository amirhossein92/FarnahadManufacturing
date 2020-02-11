﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Bars;

namespace FarnahadManufacturing.UI.Base.UserControl
{
    public abstract class UserControlBase : System.Windows.Controls.UserControl
    {
        public UserControlBase()
        {
            ToolBarItems = new Dictionary<string, IBarItem>();
        }

        public Dictionary<string, IBarItem> ToolBarItems { get; set; }

        protected abstract void SetToolBarItems();
        protected abstract void InitialData();
    }
}
