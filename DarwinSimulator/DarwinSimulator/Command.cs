using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DarwinSimulator
{
    internal class Command : ICommand
    {
        private readonly Action _command;
        private readonly Func<bool>? _canExecute;

        public Command(Action command, Func<bool>? canExecute = null)
        {
            if (command == null)
                throw new ArgumentNullException("command");
            _command = command;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object? parameter)
        {
            _command();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
