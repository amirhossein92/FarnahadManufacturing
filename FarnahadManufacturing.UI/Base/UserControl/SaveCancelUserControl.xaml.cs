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
    public partial class SaveCancelUserControl : System.Windows.Controls.UserControl
    {
        public SaveCancelUserControl()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler ClickOnSave;
        public event RoutedEventHandler ClickOnCancel;

        private void SaveButtonOnClick(object sender, RoutedEventArgs e)
        {
            ClickOnSave?.Invoke(sender, e);
        }

        private void CancelButtonOnClick(object sender, RoutedEventArgs e)
        {
            ClickOnCancel?.Invoke(sender, e);
        }
    }
}
