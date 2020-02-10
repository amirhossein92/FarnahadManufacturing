using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;
using FarnahadManufacturing.UI.Common;
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
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ActiveToolBarService.SubscribeOnActiveToolBarChange += ActiveToolBarItemsOnCollectionChanged;
        }

        private void ActiveToolBarItemsOnCollectionChanged(object sender, ActiveToolBarEventArg e)
        {
            var items = (Dictionary<string, IBarItem>)sender;
            ToolBarControl.Items.Clear();
            foreach (var item in items)
                ToolBarControl.Items.Add(item.Value);
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
