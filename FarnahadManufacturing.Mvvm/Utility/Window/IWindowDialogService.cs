using FarnahadManufacturing.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.Mvvm.Utility.Window
{
    public interface IWindowDialogService
    {
        void OpenNewDialogPage<T>(ViewModelBase currentViewModel, T viewModel = null) where T : ViewModelBase;
        void OpenNewPageDialogWithConstructor<T, Tparameter>(ViewModelBase currentViewModel, Tparameter parameter, T viewModel = null) where T : ViewModelBase<Tparameter>;
        void CloseDialogPage(ViewModelBase currentViewModel, bool result = false);
    }
}
