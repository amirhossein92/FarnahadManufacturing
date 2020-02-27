using System;
using System.ComponentModel;
using System.Windows;
using DevExpress.Xpf.Core;
using FarnahadManufacturing.Control.Common;

namespace FarnahadManufacturing.Control.Base.Window
{
    public class FmDialogWindow : DXWindow
    {
        public FmDialogWindow()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
