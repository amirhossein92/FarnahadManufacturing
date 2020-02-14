using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FarnahadManufacturing.UI.Base.UserControl
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
