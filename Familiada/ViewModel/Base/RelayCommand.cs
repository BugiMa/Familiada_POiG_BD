using System;
using System.Windows.Input;

namespace Familiada.ViewModel.Base
{
    class RelayCommand : ICommand
    {
        readonly Action<object> _execute;

        readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            else
                this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute == null ? true : this._canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this._canExecute != null) CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (this._canExecute != null) CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            this._execute(parameter);
        }
    }
}