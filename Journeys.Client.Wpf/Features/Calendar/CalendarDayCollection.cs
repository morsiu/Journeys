using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Journeys.Client.Wpf.Features.Calendar
{
    internal class CalendarDayCollection
    {
        private const int AvailableDaysCount = 31;
        private CalendarDay[] _availableDays;
        private ObservableCollection<CalendarDay> _visibleDays;
        private ReadOnlyObservableCollection<CalendarDay> _wrapper;

        public CalendarDayCollection()
        {
            _availableDays = Enumerable.Range(1, AvailableDaysCount).Select(dayInMonth => new CalendarDay(dayInMonth)).ToArray();
            _visibleDays = new ObservableCollection<CalendarDay>();
            _wrapper = new ReadOnlyObservableCollection<CalendarDay>(_visibleDays);
        }

        public ReadOnlyObservableCollection<CalendarDay> Collection { get { return _wrapper; } }

        public void Change(int year, int monthOfYear)
        {
            var newDayCount = GetDaysInMonth(year, monthOfYear);
            var visibleDayCount = _visibleDays.Count;
            if (visibleDayCount > newDayCount)
            {
                for (int idx = visibleDayCount - 1; idx >= newDayCount; --idx)
                {
                    _visibleDays.RemoveAt(idx);
                }
            }
            if (visibleDayCount < newDayCount)
            {
                for (int idx = visibleDayCount; idx < newDayCount; ++idx)
                {
                    _visibleDays.Add(_availableDays[idx]);
                }
            }
            foreach (var day in _visibleDays)
            {
                day.Change(year, monthOfYear);
            }
        }

        public void Clear()
        {
            foreach (var day in _visibleDays)
            {
                day.Clear();
            }
        }

        public void Fill(Func<int, object> contentProvider)
        {
            foreach (var day in _visibleDays)
            {
                var content = contentProvider(day.DayOfMonth);
                day.Fill(content);
            }
        }

        private int GetDaysInMonth(int year, int monthOfYear)
        {
            try
            {
                return Math.Min(CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(year, monthOfYear), AvailableDaysCount);
            }
            catch (ArgumentOutOfRangeException)
            {
                return 0;
            }
        }
    }
}
