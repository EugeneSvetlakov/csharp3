using System;
using System.Windows.Input;

namespace ProbaWPF.mvvm
{
    public class LamdaCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Action<object> _CommandAction;
        private readonly Func<object, bool> _CanExecute;

        public LamdaCommand(Action<object> CommandAction, Func<object, bool> CanExecute = null)
        {
            _CommandAction = CommandAction ?? throw new ArgumentNullException(nameof(CommandAction));
            _CanExecute = CanExecute;
        }

        public bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter) => _CommandAction(parameter);
    }
}