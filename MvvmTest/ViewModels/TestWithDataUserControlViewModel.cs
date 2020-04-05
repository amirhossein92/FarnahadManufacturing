using FarnahadManufacturing.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmTest.ViewModels
{
    public class TestWithDataUserControlViewModel : ViewModelBase<Weather>
    {
        public override void LoadCommands()
        {
        }

        public override void LoadData()
        {
        }

        public override string SetTitle()
        {
            return "Test With Data UC";
        }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string City { get; set; }
        public double Temperature { get; set; }
    }
}
