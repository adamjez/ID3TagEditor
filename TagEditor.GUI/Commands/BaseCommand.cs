using System;
using System.Windows.Input;

namespace TagEditor.GUI.Commands
{
    public abstract class BaseCommand<T> : ICommand where T : class
    {
        protected readonly T ViewModel;

        protected BaseCommand(T viewModel)
        {
            ViewModel = viewModel;
        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;
    }
}
