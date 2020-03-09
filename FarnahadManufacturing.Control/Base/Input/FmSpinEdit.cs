using System;
using System.Windows;
using DevExpress.Xpf.Editors;
using FarnahadManufacturing.Control.Base.Size;

// CHECK
namespace FarnahadManufacturing.Control.Base.Input
{
    public class FmSpinEdit : SpinEdit
    {
        public FmSpinEdit()
        {
            FlowDirection = FlowDirection.LeftToRight;
            MaskUseAsDisplayFormat = true;
            MaskType = MaskType.Numeric;
            EditValueType = typeof(double);
            AllowNullInput = true;
        }

        public static readonly DependencyProperty FmControlWidthProperty = DependencyProperty.Register(
            "FmControlWidth", typeof(FmControlWidth), typeof(FmSpinEdit), new PropertyMetadata(default(FmControlWidth), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (FmSpinEdit)d;
            if (e.NewValue != null)
            {
                var value = Convert.ToDouble(e.NewValue);
                control.Width = value == 0 ? double.NaN : value;
            }
        }

        public FmControlWidth FmControlWidth
        {
            get => (FmControlWidth)GetValue(FmControlWidthProperty);
            set => SetValue(FmControlWidthProperty, value);
        }
    }
}
