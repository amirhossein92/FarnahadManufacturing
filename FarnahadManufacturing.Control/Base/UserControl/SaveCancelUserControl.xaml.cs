using System.Windows;

namespace FarnahadManufacturing.Control.Base.UserControl
{
    public partial class SaveCancelUserControl : UserControlBase
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
