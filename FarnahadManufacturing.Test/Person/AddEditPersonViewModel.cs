using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Test.Base;
using FarnahadManufacturing.Test.Services;

namespace FarnahadManufacturing.Test.Person
{
    public class AddEditPersonViewModel : ViewModelBase
    {
        private INavigationUtility _navigationUtility;

        public AddEditPersonViewModel(INavigationUtility navigationUtility)
        {
            Title = "Add/Edit Person";
            _navigationUtility = navigationUtility;
            ShowPersonListCommand = new RelayCommand(ShowPersonList);
            ClosePersonListCommand = new RelayCommand(ClosePersonList);
        }

        private void ClosePersonList()
        {
            _navigationUtility.ClosePage(this);
            MessageUtility<Data.Person>.InvokeMethod(new Data.Person { Id = 1, Title = "Tit" });
        }

        private void ShowPersonList()
        {
            _navigationUtility.OpenNewPage<PersonListViewModel>();
        }

        public RelayCommand ShowPersonListCommand { get; private set; }
        public RelayCommand ClosePersonListCommand { get; private set; }

        public override async void LoadData()
        {

        }
    }
}
