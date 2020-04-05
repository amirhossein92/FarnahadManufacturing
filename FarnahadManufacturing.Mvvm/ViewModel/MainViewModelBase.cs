using FarnahadManufacturing.Mvvm.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.Mvvm.ViewModel
{
    public abstract class MainViewModelBase : ViewModelBase
    {
        public MainViewModelBase()
        {
            CloseTabCommand = new RelayCommand<ViewModelBase>(OnCloseTab);
        }

        private static ObservableCollection<ViewModelBase> _currentUserViewModels = new ObservableCollection<ViewModelBase>();
        public ObservableCollection<ViewModelBase> CurrentUserViewModels
        {
            get => _currentUserViewModels;
            set
            {
                _currentUserViewModels = value;
                OnPropertyChanged();
            }
        }

        public static int _currentUserViewModelIndex;
        public int CurrentUserViewModelIndex
        {
            get => _currentUserViewModelIndex;
            set
            {
                _currentUserViewModelIndex = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<ViewModelBase> CloseTabCommand { get; set; }

        private void OnCloseTab(ViewModelBase viewModel)
        {
            this.CurrentUserViewModels.Remove(viewModel);
        }
    }
}
