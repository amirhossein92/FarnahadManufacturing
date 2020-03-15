using System.Windows;
using FarnahadManufacturing.Control.Base.Layout;

// CHECK
namespace FarnahadManufacturing.Control.Base.UserControl
{
    public partial class ListBoxButtonsUserControl : FmVerticalLayoutGroup
    {
        public ListBoxButtonsUserControl()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler AddSingle;
        public event RoutedEventHandler RemoveSingle;
        public event RoutedEventHandler AddMultiple;
        public event RoutedEventHandler RemoveMultiple;

        private void AddSingleButtonOnClick(object sender, RoutedEventArgs e)
        {
            AddSingle?.Invoke(sender, e);
        }

        private void RemoveSingleButtonOnClick(object sender, RoutedEventArgs e)
        {
            RemoveSingle?.Invoke(sender, e);
        }

        private void AddMultipleButtonOnClick(object sender, RoutedEventArgs e)
        {
            AddMultiple?.Invoke(sender, e);
        }

        private void RemoveMultipleButtonOnClick(object sender, RoutedEventArgs e)
        {
            RemoveMultiple?.Invoke(sender, e);
        }
    }
}
