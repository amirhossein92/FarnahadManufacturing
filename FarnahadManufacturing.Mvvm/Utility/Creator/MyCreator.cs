using FarnahadManufacturing.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.Mvvm.Utility.Creator
{
    public static class MyCreator
    {
        public static T ObjectCreator<T>() where T : class
        {
            return Activator.CreateInstance<T>();
        }

        public static T ViewModelCreator<T>(T viewModel) where T : ViewModelBase
        {
            if (viewModel == null)
                viewModel = ObjectCreator<T>();
            return viewModel;
        }

        public static void PassParemeterToViewModel<T, Tparameter>(T viewModel, Tparameter parameter) where T : ViewModelBase<Tparameter>
        {
            if (viewModel is ViewModelBase<Tparameter> viewModelConstructor)
                viewModelConstructor.Parameter = parameter;
        }
    }
}
