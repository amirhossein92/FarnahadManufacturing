using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Editors;

namespace FarnahadManufacturing.UI.Base.Input
{
    public class FmTextEdit : TextEdit
    {
        public FmTextEdit()
        {
            IsEnabledChanged += OnIsEnabledChanged;
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
    }
}
