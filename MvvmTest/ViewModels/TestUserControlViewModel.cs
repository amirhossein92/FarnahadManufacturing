using FarnahadManufacturing.Mvvm.Command;
using FarnahadManufacturing.Mvvm.Utility.Window;
using FarnahadManufacturing.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmTest.ViewModels
{
    public class TestUserControlViewModel : ViewModelBase
    {
        private IWindowService _windowService = new MyTabWindowService();

        public TestUserControlViewModel()
        {

        }

        public RelayCommand NewTabCommand { get; set; }
        public RelayCommand NewDialogCommand { get; set; }

        public override void LoadCommands()
        {
            NewTabCommand = new RelayCommand(OnNewTab);
        }

        private void OnNewTab()
        {
            var weather = new Weather { Id = 1, City = "Tehran", Temperature = 27, };
            _windowService.OpenNewPageWithConstructor<TestWithDataUserControlViewModel, Weather>(this, weather);
        }

        public async override void LoadData()
        {
            await Task.Delay(3000);
            Test = "Test String Loaded Asyncronous after 3 seconds from ViewModel";
        }

        public override string SetTitle()
        {
            return "Test UC";
        }

        private string _test;
        public string Test
        {
            get => _test;
            set
            {
                _test = value;
                OnPropertyChanged();
            }
        }
    }
}
