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
using DevExpress.Xpf.Docking;
using FarnahadManufacturing.UI.UserControls;
using FarnahadManufacturing.UI.UserControls.BaseConfiguration;

namespace FarnahadManufacturing.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Country_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcCountry>("کشور ها");
        }

        private void City_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenUserControlInNewTab<UcCity>("شهر ها");
        }

        private void OpenUserControlInNewTab<T>(string tabHeader) where T : UserControl
        {
            if (!UserControlIsAlreadyOpen<T>())
            {
                var panel = new DocumentPanel();
                panel.TabCaption = tabHeader;
                panel.AllowClose = true;
                panel.Content = Activator.CreateInstance<T>();
                MyDocumentGroup.Items.Add(panel);
                panel.IsActive = true;
            }
        }

        private bool UserControlIsAlreadyOpen<T>() where T : UserControl
        {
            foreach (var item in MyDocumentGroup.Items)
            {
                if (item is DocumentPanel documentPanel && documentPanel.Content is T)
                    return true;
            }
            return false;
        }
    }
}
