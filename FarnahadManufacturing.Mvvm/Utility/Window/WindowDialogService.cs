using FarnahadManufacturing.Mvvm.Utility.Creator;
using FarnahadManufacturing.Mvvm.ViewModel;
using FarnahadManufacturing.Mvvm.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace FarnahadManufacturing.Mvvm.Utility.Window
{
    public class WindowDialogService : IWindowDialogService
    {
        public void OpenNewDialogPage<T>(ViewModelBase currentViewModel, T viewModel = null) where T : ViewModelBase
        {
            var newWindow = CreateDialogWindow();
            viewModel = MyCreator.ViewModelCreator(viewModel);
            SetDialogWindowProperties(newWindow, viewModel);
            newWindow.ShowDialog();
        }

        public void OpenNewPageDialogWithConstructor<T, Tparameter>(ViewModelBase currentViewModel, Tparameter parameter, T viewModel = null) where T : ViewModelBase<Tparameter>
        {
            var newWindow = CreateDialogWindow();
            viewModel = MyCreator.ViewModelCreator(viewModel);
            MyCreator.PassParemeterToViewModel(viewModel, parameter);
            SetDialogWindowProperties(newWindow, viewModel);
            newWindow.ShowDialog();
        }

        public void CloseDialogPage(ViewModelBase currentViewModel, bool result = false)
        {
            var windows = System.Windows.Application.Current.Windows;
            var window = windows.OfType<MyDialogWindow>().LastOrDefault();
            if (window != null && window.Content.GetType() == currentViewModel.GetType())
                window.Close();
        }

        private MyDialogWindow CreateDialogWindow()
        {
            //var resources = Application.Current.MainWindow.Resources;
            var newWindow = new MyDialogWindow();
            newWindow.SizeToContent = SizeToContent.WidthAndHeight;
            //newWindow.Resources = resources;

            return newWindow;
        }

        private void SetDialogWindowProperties<T>(MyDialogWindow newWindow, T viewModel) where T : ViewModelBase
        {
            newWindow.Title = viewModel.Title;
            newWindow.Content = viewModel;
        }
    }
}
