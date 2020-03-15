using System.Windows;

namespace FarnahadManufacturing.Control.Base.UserControl
{
    public partial class AddEditDeleteRefreshButtonListUserControl : System.Windows.Controls.UserControl
    {
        public AddEditDeleteRefreshButtonListUserControl()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler AddClick;
        public event RoutedEventHandler EditClick;
        public event RoutedEventHandler DeleteClick;
        public event RoutedEventHandler RefreshClick;

        private void AddButtonOnClick(object sender, RoutedEventArgs e)
        {
            AddClick?.Invoke(sender, e);
        }

        private void EditButtonOnClick(object sender, RoutedEventArgs e)
        {
            EditClick?.Invoke(sender, e);
        }

        private void DeleteButtonOnClick(object sender, RoutedEventArgs e)
        {
            DeleteClick?.Invoke(sender, e);
        }

        private void RefreshButtonOnClick(object sender, RoutedEventArgs e)
        {
            RefreshClick?.Invoke(sender, e);
        }
    }
}
