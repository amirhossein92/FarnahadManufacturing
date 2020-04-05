using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FarnahadManufacturing.Mvvm.Command
{
    public class RelayCommand : ICommand
    {
        public Action _execute;
        public Func<bool> _canExecute;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute.Invoke();

            return true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        public Action<T> _execute;
        public Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute)
        {
            _execute = execute;
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute.Invoke((T)parameter);

            return true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke((T)parameter);
        }
    }
}
