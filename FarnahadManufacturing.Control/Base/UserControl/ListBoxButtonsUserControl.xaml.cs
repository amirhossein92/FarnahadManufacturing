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
using FarnahadManufacturing.Control.Base.Layout;

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
