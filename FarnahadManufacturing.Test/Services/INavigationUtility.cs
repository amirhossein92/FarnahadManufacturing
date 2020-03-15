using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Test.Base;

namespace FarnahadManufacturing.Test.Services
{
    public interface INavigationUtility
    {
        void OpenNewPage<T>() where T : ViewModelBase;
        void OpenNewPageDialog<T>(ViewModelBase owner) where T : ViewModelBase;
        void CloseNewPageDialog<T>(T viewModel) where T : ViewModelBase;
        void ClosePage<T>(T viewModel) where T : ViewModelBase;
        Action<Type> NavigationOpenAction { get; set; }
        Action<ViewModelBase> NavigationCloseAction { get; set; }
        Action<Type> NavigationOpenDialogAction { get; set; }
        Action<ViewModelBase> NavigationCloseDialogAction { get; set; }
    }
}
