using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.Mvvm.ViewModel
{
    public class ViewModelCollection : IEnumerable<ViewModelBase>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private ObservableCollection<ViewModelBase> _viewModels;

        public ViewModelCollection()
        {
            _viewModels = new ObservableCollection<ViewModelBase>();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerator<ViewModelBase> GetEnumerator()
        {
            return _viewModels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _viewModels.GetEnumerator();
        }

        public void Add(ViewModelBase viewModelBase)
        {
            _viewModels.Add(viewModelBase);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
        }
    }
}
