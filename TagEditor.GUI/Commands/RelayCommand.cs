using System;
using System.Windows.Input;

namespace TagEditor.GUI.Commands
{
    public class RelayCommand : ICommand
    {
        private Action action;

        public RelayCommand(Action action)
        {
            this.action = action;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(Object parameter) => true;

        public void Execute(Object parameter)
        {
            action.Invoke();
        }
    }
}