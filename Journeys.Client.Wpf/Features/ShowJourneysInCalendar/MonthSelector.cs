using System;
using System.ComponentModel;
using Journeys.Client.Wpf.Infrastructure;
using Journeys.Client.Wpf.Infrastructure.Extensions;

namespace Journeys.Client.Wpf.Features.ShowJourneysInCalendar
{
    internal class MonthSelector : INotifyPropertyChanged
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
