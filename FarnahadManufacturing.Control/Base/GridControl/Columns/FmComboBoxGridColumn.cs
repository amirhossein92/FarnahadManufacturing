using System.Windows;
using DevExpress.Xpf.Editors;

namespace FarnahadManufacturing.Control.Base.GridControl.Columns
{
    public class FmComboBoxGridColumn : FmGridColumn
    {
        public FmComboBoxGridColumn()
        {
            EditSettings = new FmComboBoxEditSettings
            {
                AllowNullInput = true,
                NullValueButtonPlacement = EditorPlacement.EditBox
            };
        }

        public static readonly DependencyProperty ValueMemberProperty = DependencyProperty.Register(
            "ValueMember", typeof(string), typeof(FmComboBoxGridColumn),
            new PropertyMetadata(default(string), ValueMemberPropertyChangedCallback));

        private static void ValueMemberPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var comboBox = (FmComboBoxGridColumn)d;
                if (comboBox.EditSettings is FmComboBoxEditSettings comboBoxEditSettings)
                    comboBoxEditSettings.ValueMember = e.NewValue.ToString();
            }
        }

        public string ValueMember
        {
            get => (string)GetValue(ValueMemberProperty);
            set => SetValue(ValueMemberProperty, value);
        }

        public static readonly DependencyProperty DisplayMemberProperty = DependencyProperty.Register(
            "DisplayMember", typeof(string), typeof(FmComboBoxGridColumn),
            new PropertyMetadata(default(string), DisplayMemberPropertyChangedCallback));

        private static void DisplayMemberPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var comboBox = (FmComboBoxGridColumn)d;
                if (comboBox.EditSettings is FmComboBoxEditSettings comboBoxEditSettings)
                    comboBoxEditSettings.DisplayMember = e.NewValue.ToString();
            }
        }

        public string DisplayMember
        {
            get => (string)GetValue(DisplayMemberProperty);
            set => SetValue(DisplayMemberProperty, value);
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(object), typeof(FmComboBoxGridColumn),
            new PropertyMetadata(default(string), ItemsSourcePropertyChangedCallback));

        private static void ItemsSourcePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var comboBox = (FmComboBoxGridColumn)d;
                if (comboBox.EditSettings is FmComboBoxEditSettings comboBoxEditSettings)
                    comboBoxEditSettings.ItemsSource = e.NewValue;
            }
        }

        public object ItemsSource
        {
            get => (object)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
    }
}
