using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Editors;

namespace FarnahadManufacturing.UI.Base.Input
{
    public class FmComboBoxEdit : ComboBoxEdit
    {
        public FmComboBoxEdit()
        {
            IsEnabledChanged += OnIsEnabledChanged;
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var comboBoxEdit = (FmComboBoxEdit)sender;
            var enabled = (bool)e.NewValue;
            if (enabled && comboBoxEdit.Text == ControlDefaultValue.ControlIsDisable)
                Text = string.Empty;
            else if (!enabled && string.IsNullOrEmpty(comboBoxEdit.Text))
                Text = ControlDefaultValue.ControlIsDisable;
        }
    }
}
