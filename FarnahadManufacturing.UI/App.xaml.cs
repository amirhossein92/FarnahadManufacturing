using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.Core;
using FarnahadManufacturing.Control.Common;
using FarnahadManufacturing.UI.Assets;
using FarnahadManufacturing.UI.UserControls.Login;

// CHECK
namespace FarnahadManufacturing.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.VS2017LightName;
            base.OnStartup(e);
            DXSplashScreen.Show<SplashScreenView>();
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            // LOCALIZATION
            DXMessageBoxLocalizer.Active = new MessageBoxLocalizer();
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            if (exception != null)
                MessageBoxService.ShowError($"{e.Exception.Message}", "Error");
            e.Handled = true;
        }
    }

    public class MessageBoxLocalizer : DXMessageBoxLocalizer
    {
        protected override void PopulateStringTable()
        {
            base.PopulateStringTable();
            AddString(DXMessageBoxStringId.Ok, "تایید");
            AddString(DXMessageBoxStringId.No, "خیر");
            AddString(DXMessageBoxStringId.Cancel, "لغو");
            AddString(DXMessageBoxStringId.Yes, "بله");
        }

    }
}
