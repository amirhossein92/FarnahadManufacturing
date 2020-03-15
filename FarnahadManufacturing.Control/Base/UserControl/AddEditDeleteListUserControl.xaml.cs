using System.Windows;

namespace FarnahadManufacturing.Control.Base.UserControl
{
    public partial class AddEditDeleteListUserControl : System.Windows.Controls.UserControl
    {
        public AddEditDeleteListUserControl()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler ClickOnAddItem;
        public event RoutedEventHandler ClickOnEditItem;
        public event RoutedEventHandler ClickOnDeleteItem;

        private void AddItemButtonOnClick(object sender, RoutedEventArgs e)
        {
            ClickOnAddItem?.Invoke(sender, e);
        }

        private void EditItemButtonOnClick(object sender, RoutedEventArgs e)
        {
            ClickOnEditItem?.Invoke(sender, e);
        }

        private void DeleteItemButtonOnClick(object sender, RoutedEventArgs e)
        {
            ClickOnDeleteItem?.Invoke(sender, e);
        }
    }
}
