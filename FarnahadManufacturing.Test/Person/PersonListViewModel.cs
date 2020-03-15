using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Test.Base;
using FarnahadManufacturing.Test.Services;
using Microsoft.Win32;

namespace FarnahadManufacturing.Test.Person
{
    public class PersonListViewModel : ViewModelBase
    {
        private INavigationUtility _navigationUtility;

        public PersonListViewModel(INavigationUtility navigationUtility)
        {
            Title = "Person List";
            _navigationUtility = navigationUtility;
            AddPersonCommand = new RelayCommand(OnAddPerson);
        }

        public override async void LoadData()
        {
            await Task.Delay(5000);
            Test = "I am the dataaaaaa......";
        }

        private void OnAddPerson()
        {
            MessageUtility<Data.Person>.Subscribe(Action);
            _navigationUtility.OpenNewPage<AddEditPersonViewModel>();
        }

        private void Action(Data.Person person)
        {
            var b = 2;
        }

        private string _test;
        public string Test
        {
            get => _test;
            set => SetValue(ref _test, value);
        }

        public RelayCommand AddPersonCommand { get; private set; }
    }
}
