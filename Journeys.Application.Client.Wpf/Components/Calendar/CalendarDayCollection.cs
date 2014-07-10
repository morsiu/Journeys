using Journeys.Application.Client.Wpf.Infrastructure.Extensions;
using Journeys.Application.Client.Wpf.Infrastructure.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

namespace Journeys.Application.Client.Wpf.Components.Calendar
{
    internal sealed class CalendarDayCollection : IChangeNotifyReadOnlyList<CalendarDay>
    {
        private const int AvailableDaysCount = 31;
        private CalendarDay[] _availableDays;
        private int _dayCount;

        public CalendarDayCollection()
        {
            _availableDays = Enumerable.Range(1, AvailableDaysCount).Select(dayInMonth => new CalendarDay(dayInMonth)).ToArray();
            _dayCount = 0;
        }

        public void Change(int year, int monthOfYear)
        {
            var newDayCount = GetNewDayCount(year, monthOfYear);
            var oldDayCount = _dayCount;
            _dayCount = newDayCount;
            foreach (var day in this)
            {
                day.Change(year, monthOfYear);
            }
            var dayCountDifference = Math.Abs(newDayCount - oldDayCount);
            if (oldDayCount < newDayCount)
            {
                var addedItems = new CalendarDay[dayCountDifference];
                Array.Copy(_availableDays, oldDayCount, addedItems, 0, dayCountDifference);
                CollectionChanged.Raise(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, addedItems, oldDayCount));
            }
            if (oldDayCount > newDayCount)
            {
                var removedItems = new CalendarDay[dayCountDifference];
                Array.Copy(_availableDays, newDayCount, removedItems, 0, dayCountDifference);
                CollectionChanged.Raise(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItems, newDayCount));
            }
        }

        public void Clear()
        {
            foreach (var day in this)
            {
                day.Clear();
            }
        }

        public void Fill(Func<int, object> contentProvider)
        {
            foreach (var day in this)
            {
                var content = contentProvider(day.DayOfMonth);
                day.Fill(content);
            }
        }

        private int GetNewDayCount(int year, int monthOfYear)
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

        public int Count
        {
            get { return _dayCount; }
        }

        public IEnumerator<CalendarDay> GetEnumerator()
        {
            for(int idx = 0; idx < _dayCount; ++idx)
            {
                yield return _availableDays[idx];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public CalendarDay this[int index]
        {
            get
            {
                if (index > _dayCount) throw new ArgumentOutOfRangeException("index");
                return _availableDays[index];
            }
        }
    }
}
