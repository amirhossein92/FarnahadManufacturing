using FarnahadManufacturing.Mvvm.Utility.Window;
using FarnahadManufacturing.Mvvm.ViewModel;
using MvvmTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmTest
{
    public class MainWindowViewModel : MainViewModelBase
    {
        private IWindowService _tabWindowService = new MyTabWindowService();

        public MainWindowViewModel()
        {
            _tabWindowService.SetMainWindowViewModel(this);
            _tabWindowService.OpenNewPage<TestUserControlViewModel>(null);
        }

        public override void LoadCommands()
        {
        }

        public override void LoadData()
        {
        }

        public override string SetTitle()
        {
            return "Main Window";
        }

    }
}
