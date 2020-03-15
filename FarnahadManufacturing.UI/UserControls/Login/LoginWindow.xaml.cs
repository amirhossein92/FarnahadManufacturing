using System.Windows;
using DevExpress.Xpf.Core;
using FarnahadManufacturing.Data;

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
