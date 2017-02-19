using System;
using System.Windows.Input;

namespace Couple.Command
{
    public class DelegateCommand : ICommand
    {
        protected readonly Action<object> _execute;
        protected readonly Predicate<object> _canExecute;

        // Constructor
        public DelegateCommand(Action<object> execute)
        : this(execute, null)
        {
        }
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        // Event required by ICommand
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
