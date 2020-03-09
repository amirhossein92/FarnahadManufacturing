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
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using FarnahadManufacturing.Data;
using FarnahadManufacturing.UI.Assets;

// CHECK
namespace FarnahadManufacturing.UI.UserControls.Login
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                dbContext.Database.Initialize(true);
            }
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            DXSplashScreen.Close();
            this.Activate();
        }

        private void ExitButtonOnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoginButtonOnClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
            mainWindow.Activate();
        }
    }
}
