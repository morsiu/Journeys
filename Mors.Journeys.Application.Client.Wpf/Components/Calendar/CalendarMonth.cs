﻿using System;
using System.ComponentModel;
using Mors.Journeys.Application.Client.Wpf.Infrastructure.Extensions;
using Mors.Journeys.Application.Client.Wpf.Infrastructure.Interfaces;

namespace Mors.Journeys.Application.Client.Wpf.Components.Calendar
{
    internal sealed class CalendarMonth : INotifyPropertyChanged
    {
        private readonly CalendarDayCollection _days = new CalendarDayCollection();
        private int _monthOfYear;
        private int _year;

        public INotifyCollectionChangedReadOnlyList<CalendarDay> Days { get { return _days; } }

        public int MonthOfYear
        {
            get { return _monthOfYear; }
            set
            {
                _monthOfYear = value;
                PropertyChanged.Raise(this);
            }
        }

        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                PropertyChanged.Raise(this);
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
