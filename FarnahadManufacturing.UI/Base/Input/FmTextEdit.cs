using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Editors;
using DevExpress.XtraPrinting.Export;
using FarnahadManufacturing.UI.Base.Size;

namespace FarnahadManufacturing.UI.Base.Input
{
    public class FmTextEdit : TextEdit
    {
        public FmTextEdit()
        {
            IsEnabledChanged += OnIsEnabledChanged;
            // As Auto
            Width = double.NaN;
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
                control.Width = Convert.ToDouble(e.NewValue);
        }

        public FmControlWidth FmControlWidth
        {
            get { return (FmControlWidth)GetValue(FmControlWidthProperty); }
            set { SetValue(FmControlWidthProperty, value); }
        }
    }
}
