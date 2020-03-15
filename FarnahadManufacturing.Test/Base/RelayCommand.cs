using System;
using System.Windows.Input;

namespace FarnahadManufacturing.Test.Base
{
    public class RelayCommand : ICommand
    {
        private Action _executeFunction;
        private Func<bool> _canExecute;

        private RelayCommand()
        {

        }

        public RelayCommand(Action execute)
        {
            _executeFunction = execute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _executeFunction = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute.Invoke();
            else
                return true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _executeFunction.Invoke();
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
