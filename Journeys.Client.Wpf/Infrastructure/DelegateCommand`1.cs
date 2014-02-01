using System;
using System.Windows.Input;

namespace Journeys.Client.Wpf.Infrastructure
{
    internal class DelegateCommand<TParameter> : ICommand
    {
        private readonly Action<TParameter> _handler;

        public DelegateCommand(Action<TParameter> handler)
        {
            if (handler == null) throw new ArgumentNullException("handler");
            _handler = handler;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public void Execute(object parameter)
        {
            if (parameter is TParameter)
            {
                _handler((TParameter)parameter);
            }
        }
    }
}
