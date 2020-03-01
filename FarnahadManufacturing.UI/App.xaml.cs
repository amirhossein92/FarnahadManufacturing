using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Core;
using FarnahadManufacturing.UI.Assets;
using FarnahadManufacturing.UI.UserControls.Login;

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
        }
    }
}
