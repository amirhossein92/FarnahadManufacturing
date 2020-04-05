using FarnahadManufacturing.Mvvm.Utility.Creator;
using FarnahadManufacturing.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.Mvvm.Utility.Window
{
    public class MyTabWindowService : IWindowService
    {
        private static MainViewModelBase _mainViewModel;
        public MainViewModelBase MainViewModel { get => _mainViewModel; }

        public void SetMainWindowViewModel(MainViewModelBase mainWindowViewModel)
        {
            _mainViewModel = mainWindowViewModel;
        }

        public void ClosePage(ViewModelBase currentViewModel)
        {
            _mainViewModel.CurrentUserViewModels.Remove(currentViewModel);
        }

        public void OpenNewPage<T>(ViewModelBase currentViewModel, T viewModel = null) where T : ViewModelBase
        {
            viewModel = MyCreator.ViewModelCreator<T>(viewModel);

            if (_mainViewModel.CurrentUserViewModels.OfType<T>().Any())
            {
                var vm = _mainViewModel.CurrentUserViewModels.OfType<T>().First();
                _mainViewModel.CurrentUserViewModelIndex = _mainViewModel.CurrentUserViewModels.IndexOf(vm);
            }
            else
            {
                _mainViewModel.CurrentUserViewModels.Add(viewModel);
                var index = _mainViewModel.CurrentUserViewModels.IndexOf(viewModel);
                _mainViewModel.CurrentUserViewModelIndex = index;
            }
        }

        public void OpenNewPageWithConstructor<T, Tparameter>(ViewModelBase currentViewModel, Tparameter parameter, T viewModel = null) where T : ViewModelBase<Tparameter>
        {
            viewModel = MyCreator.ViewModelCreator<T>(viewModel);
            MyCreator.PassParemeterToViewModel(viewModel, parameter);

            if (_mainViewModel.CurrentUserViewModels.OfType<T>().Any())
            {
                var vm = _mainViewModel.CurrentUserViewModels.OfType<T>().First();
                _mainViewModel.CurrentUserViewModelIndex = _mainViewModel.CurrentUserViewModels.IndexOf(vm);
            }
            else
            {
                _mainViewModel.CurrentUserViewModels.Add(viewModel);
                var index = _mainViewModel.CurrentUserViewModels.IndexOf(viewModel);
                _mainViewModel.CurrentUserViewModelIndex = index;
            }
        }
    }
}
