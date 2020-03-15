using System.Windows;
using FarnahadManufacturing.Test.Services;

namespace FarnahadManufacturing.Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            var mainWindowViewModel = new MainWindowViewModel(new NavigationUtility());
            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.ShowDialog();
        }
    }
}
