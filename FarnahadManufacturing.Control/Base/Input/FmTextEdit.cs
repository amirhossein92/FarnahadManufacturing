using System;
using System.Windows;
using DevExpress.Xpf.Editors;
using FarnahadManufacturing.Control.Base.Size;

namespace FarnahadManufacturing.Control.Base.Input
{
    public class FmTextEdit : TextEdit
    {
        public FmTextEdit()
        {
            IsEnabledChanged += OnIsEnabledChanged;
            FmControlWidth = FmControlWidth.Auto;
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var textEdit = (TextEdit)sender;
            var enabled = (bool)e.NewValue;
            if (enabled && textEdit.Text == ControlDefaultValue.ControlIsDisable)
                Text = string.Empty;
            else if (!enabled && string.IsNullOrEmpty(textEdit.Text))
                Text = ControlDefaultValue.ControlIsDisable;
        }

        public static readonly DependencyProperty FmControlWidthProperty = DependencyProperty.Register(
            "FmControlWidth", typeof(FmControlWidth), typeof(FmTextEdit), new PropertyMetadata(default(FmControlWidth), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (FmTextEdit)d;
            if (e.NewValue != null)
            {
                var value = Convert.ToDouble(e.NewValue);
                control.Width = value == 0 ? double.NaN : value;
            }
        }

        public FmControlWidth FmControlWidth
        {
            get { return (FmControlWidth)GetValue(FmControlWidthProperty); }
            set { SetValue(FmControlWidthProperty, value); }
        }
    }
}
