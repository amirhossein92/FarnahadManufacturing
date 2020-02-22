﻿using System.Windows;
using DevExpress.Xpf.Editors;

namespace FarnahadManufacturing.Control.Base.Input
{
    public class FmComboBoxEdit : ComboBoxEdit
    {
        public FmComboBoxEdit()
        {
            AllowNullInput = true;
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