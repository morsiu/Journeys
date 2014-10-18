using System;
using System.Windows.Input;

namespace Journeys.Application.Client.Wpf.Infrastructure
{
    internal sealed class DelegateCommand : ICommand
    {
        private readonly Action _handler;

        public DelegateCommand(Action handler)
        {
            if (handler == null) throw new ArgumentNullException("handler");
            _handler = handler;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged { add {} remove {} }

        public void Execute(object parameter)
        {
            _handler();
        }
    }
}
