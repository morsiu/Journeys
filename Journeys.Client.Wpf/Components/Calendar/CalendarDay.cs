using System;
using System.ComponentModel;
using Journeys.Client.Wpf.Infrastructure.Extensions;

namespace Journeys.Client.Wpf.Components.Calendar
{
    internal class CalendarDay : INotifyPropertyChanged
    {
        private readonly int _dayOfMonth;
        private object _content;
        private DateTime _date;

        public CalendarDay(int dayOfMonth)
        {
            _dayOfMonth = dayOfMonth;
        }

        public void Clear()
        {
            Content = null;
        }

        public void Change(int year, int month)
        {
            _date = new DateTime(year, month, _dayOfMonth);
        }

        public void Fill(object content)
        {
            Content = content;
        }

        public object Content
        {
            get { return _content; }
            private set
            {
                _content = value;
                PropertyChanged.Raise();
            }
        }

        public int DayOfMonth
        {
            get { return _dayOfMonth; }
        }

        public DateTime Date
        {
            get { return _date; }
            private set
            {
                _date = value;
                PropertyChanged.Raise();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
