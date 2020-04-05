using FarnahadManufacturing.Mvvm.Utility.Extensions;
using FarnahadManufacturing.Mvvm.ViewModel;
using MvvmTest.View;
using MvvmTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MvvmTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var resources = new List<KeyValuePair<object, object>>();
            resources.Add(MyViewLocator.BindViewModelToView<TestUserControlViewModel, TestUserControl>());
            resources.Add(MyViewLocator.BindViewModelToView<TestWithDataUserControlViewModel, TestWithDataUserControlView>());

            foreach (var resource in resources)
                this.Resources.Add(resource.Key, resource.Value);
        }
    }
}
