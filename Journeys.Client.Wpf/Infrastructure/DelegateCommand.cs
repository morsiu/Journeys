using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Journeys.Client.Wpf.Infrastructure
{
    internal class DelegateCommand : ICommand
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
