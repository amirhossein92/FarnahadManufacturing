using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Core;
using FarnahadManufacturing.Test.Base;

namespace FarnahadManufacturing.Test.Services
{
    public class NavigationUtility : INavigationUtility
    {
        public NavigationUtility()
        {
        }

        public void OpenNewPage<T>() where T : ViewModelBase
        {
            //var viewModel = Activator.CreateInstance<T>();
            //_mainWindowViewModel.CurrentViewModel = viewModel;
            NavigationOpenAction?.Invoke(typeof(T));
        }

        public void OpenNewPageDialog<T>(ViewModelBase owner) where T : ViewModelBase
        {
            NavigationOpenDialogAction?.Invoke(typeof(T));
        }

        public void CloseNewPageDialog<T>(T viewModel) where T : ViewModelBase
        {
            NavigationCloseDialogAction?.Invoke(viewModel);
        }

        public void ClosePage<T>(T viewModel) where T : ViewModelBase
        {
            NavigationCloseAction?.Invoke(viewModel);
        }

        public Action<Type> NavigationOpenAction { get; set; }
        public Action<ViewModelBase> NavigationCloseAction { get; set; }
        public Action<Type> NavigationOpenDialogAction { get; set; }
        public Action<ViewModelBase> NavigationCloseDialogAction { get; set; }
    }
}
