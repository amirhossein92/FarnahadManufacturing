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

        public void OpenNewPage<TViewModel>(ViewModelBase currentViewModel, TViewModel viewModel = null) where TViewModel : ViewModelBase
        {
            viewModel = MyCreator.ViewModelCreator<TViewModel>(viewModel);

            if (_mainViewModel.CurrentUserViewModels.OfType<TViewModel>().Any())
            {
                var vm = _mainViewModel.CurrentUserViewModels.OfType<TViewModel>().First();
                _mainViewModel.CurrentUserViewModelIndex = _mainViewModel.CurrentUserViewModels.IndexOf(vm);
            }
            else
            {
                _mainViewModel.CurrentUserViewModels.Add(viewModel);
                var index = _mainViewModel.CurrentUserViewModels.IndexOf(viewModel);
                _mainViewModel.CurrentUserViewModelIndex = index;
            }
        }

        public void OpenNewPageWithConstructor<TViewModel, Tparameter>(ViewModelBase currentViewModel, Tparameter parameter, TViewModel viewModel = null) where TViewModel : ViewModelBase<Tparameter>
        {
            viewModel = MyCreator.ViewModelCreator<TViewModel>(viewModel);
            MyCreator.PassParemeterToViewModel(viewModel, parameter);

            if (_mainViewModel.CurrentUserViewModels.OfType<TViewModel>().Any())
            {
                var vm = _mainViewModel.CurrentUserViewModels.OfType<TViewModel>().First();
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
