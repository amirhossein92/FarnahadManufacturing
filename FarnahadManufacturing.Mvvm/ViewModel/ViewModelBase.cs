using FarnahadManufacturing.Mvvm.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.Mvvm.ViewModel
{
    public abstract class ViewModelBase : BindableBase
    {
        public ViewModelBase()
        {
            Title = SetTitle();
            LoadCommands();
        }

        public abstract string SetTitle();
        public abstract void LoadCommands();
        public abstract void LoadData();

        private string _title;
        public string Title
        {
            get => _title; set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }

    public abstract class ViewModelBase<T> : ViewModelBase
    {
        private T _parameter;
        public T Parameter
        {
            get => _parameter; set
            {
                _parameter = value;
                OnPropertyChanged();
            }
        }
    }
}
