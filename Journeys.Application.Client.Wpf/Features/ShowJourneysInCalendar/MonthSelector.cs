using System;
using System.ComponentModel;
using System.Windows.Input;
using Journeys.Application.Client.Wpf.Infrastructure;
using Journeys.Application.Client.Wpf.Infrastructure.Extensions;

namespace Journeys.Application.Client.Wpf.Features.ShowJourneysInCalendar
{
    internal sealed class MonthSelector : INotifyPropertyChanged
    {
        private Month _current;

        public MonthSelector(Month current)
        {
            _current = current;
        }

        public void Select(Month month) 
        {
            Current = month;
        }

        public void Next() 
        {
            Current = Current.Next();
        }

        public void Previous() 
        {
            Current = Current.Previous();
        }

        public ICommand NextCommand
        {
            get { return new DelegateCommand(Next); }
        }

        public ICommand PreviousCommand
        {
            get { return new DelegateCommand(Previous); }
        }

        public Month Current
        {
            get { return _current; }
            private set
            {
                _current = value;
                PropertyChanged.Raise(this);
                CurrentChanged.Raise(this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler CurrentChanged;
    }
}
