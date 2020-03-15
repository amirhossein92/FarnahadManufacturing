using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Xpf.Core;
using FarnahadManufacturing.Test.Base;
using FarnahadManufacturing.Test.Person;
using FarnahadManufacturing.Test.Services;

namespace FarnahadManufacturing.Test
{
    public class MainWindowViewModel : BindableBase
    {
        private INavigationUtility _navigationUtility;

        public MainWindowViewModel(INavigationUtility navigationUtility)
        {
            _navigationUtility = navigationUtility;
            _navigationUtility.NavigationOpenAction += NavigationOpen;
            _navigationUtility.NavigationCloseAction += NavigationClose;
            _navigationUtility.NavigationOpenDialogAction += NavigationOpenDialog;
            _navigationUtility.NavigationCloseDialogAction += NavigationCloseDialog;
            GoToFirstTabCommand = new RelayCommand(OnGoToFirstTab);
            OpenPersonListCommand = new RelayCommand(OnOpenPersonList, CanOpenPersonList);
            CloseCurrentViewModelCommand = new RelayCommand(OnCloseCurrentViewModel);
        }

        public ICommand GoToFirstTabCommand { get; set; }
        public ICommand OpenPersonListCommand { get; set; }
        public ICommand CloseCurrentViewModelCommand { get; set; }

        private void OnGoToFirstTab()
        {
            CurrentViewModelIndex = 0;
        }

        private void OnOpenPersonList()
        {
            _navigationUtility.OpenNewPage<PersonListViewModel>();
            _navigationUtility.OpenNewPage<AddEditPersonViewModel>();
        }

        private void OnCloseCurrentViewModel()
        {
            CurrentViewModels.RemoveAt(CurrentViewModelIndex);
        }

        private bool CanOpenPersonList()
        {
            return true;
        }

        private void NavigationOpen(Type viewModelType)
        {
            var type = this.GetType();
            var openNewPageMethod = type.GetMethod("OpenNewPage");
            var generic = openNewPageMethod.MakeGenericMethod(viewModelType);
            generic.Invoke(this, null);
        }

        public void OpenNewPage<T>() where T : ViewModelBase
        {
            // TODO: USE DI
            var viewModel = (T)Activator.CreateInstance(typeof(T), _navigationUtility);
            CurrentViewModel = viewModel;
            CurrentViewModels.Add(CurrentViewModel);
            CurrentViewModelIndex = CurrentViewModels.IndexOf(CurrentViewModel);
        }

        private void NavigationClose(ViewModelBase viewModel)
        {
            var currentViewModel = CurrentViewModels.FirstOrDefault(item => item == viewModel);
            CurrentViewModels.Remove(currentViewModel);
        }

        private void NavigationOpenDialog(Type viewModelType)
        {
            var viewModel = Activator.CreateInstance(viewModelType, _navigationUtility);
            var window = new DXWindow();
            window.Content = viewModel;
        }

        private void NavigationCloseDialog(ViewModelBase viewModel)
        {
            throw new NotImplementedException();
        }

        private ObservableCollection<ViewModelBase> _currentViewModels = new ObservableCollection<ViewModelBase>();
        public ObservableCollection<ViewModelBase> CurrentViewModels
        {
            get => _currentViewModels;
            set => SetValue(ref _currentViewModels, value);
        }

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetValue(ref _currentViewModel, value);
        }

        private int _currentViewModelIndex;
        public int CurrentViewModelIndex
        {
            get => _currentViewModelIndex;
            set => SetValue(ref _currentViewModelIndex, value);
        }
    }
}
