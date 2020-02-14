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
    public class FmTextGridColumn : GridColumn
    {
        public FmTextGridColumn()
        {

        }

        public static readonly DependencyProperty FmControlWidthProperty = DependencyProperty.Register(
            "FmControlWidth", typeof(FmControlWidth), typeof(FmTextGridColumn), new PropertyMetadata(default(FmControlWidth), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (FmTextGridColumn)d;
            if (e.NewValue != null)
                control.Width = Convert.ToDouble(e.NewValue);
        }

        public FmControlWidth FmControlWidth
        {
            get { return (FmControlWidth)GetValue(FmControlWidthProperty); }
            set { SetValue(FmControlWidthProperty, value); }
        }
    }
}
