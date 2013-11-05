using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Journeys.Client.Wpf.Infrastructure.Extensions;

namespace Journeys.Client.Wpf.Features.Calendar
{
    internal class CalendarMonth : INotifyPropertyChanged
    {
        private readonly CalendarDayCollection _days = new CalendarDayCollection();
        private int _monthOfYear;
        private int _year;

        public ReadOnlyObservableCollection<CalendarDay> Days { get { return _days.Collection; } }

        public int MonthOfYear
        {
            get { return _monthOfYear; }
            set
            {
                _monthOfYear = value;
                PropertyChanged.Raise();
            }
        }

        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                PropertyChanged.Raise();
            }
        }

        public void Clear()
        {
            _days.Clear();
        }

        public void Fill(Func<int, object> contentProvider)
        {
            _days.Fill(contentProvider);
        }

        public void Change(int year, int monthOfYear)
        {
            Year = year;
            MonthOfYear = monthOfYear;
            _days.Change(year, monthOfYear);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
