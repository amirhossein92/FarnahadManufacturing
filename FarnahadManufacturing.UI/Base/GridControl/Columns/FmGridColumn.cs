using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Grid;
using FarnahadManufacturing.UI.Base.Size;

namespace FarnahadManufacturing.UI.Base.GridControl.Columns
{
    public class FmGridColumn : GridColumn
    {
        public static readonly DependencyProperty FmControlWidthProperty = DependencyProperty.Register(
            "FmControlWidth", typeof(FmControlWidth), typeof(FmTextGridColumn), new PropertyMetadata(default(FmControlWidth), PropertyChangedCallback));

        protected static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (FmGridColumn)d;
            if (e.NewValue != null)
                control.Width = Convert.ToDouble(e.NewValue);
        }

        public FmControlWidth FmControlWidth
        {
            get => (FmControlWidth)GetValue(FmControlWidthProperty);
            set => SetValue(FmControlWidthProperty, value);
        }
    }
}
