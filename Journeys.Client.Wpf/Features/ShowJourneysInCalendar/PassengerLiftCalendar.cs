using System;
using Journeys.Client.Wpf.Components.Calendar;
using Data = Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay;

namespace Journeys.Client.Wpf.Features.ShowJourneysInCalendar
{
    internal class PassengerLiftCalendar
    {
        private readonly CalendarContentProvider _contentProvider;
        private readonly Passenger _passenger;
        private readonly MonthSelector _monthSelector;
        private readonly CalendarMonth _monthCalendar;

        public PassengerLiftCalendar(Passenger passenger, Month initialMonth, CalendarContentProvider contentProvider)
        {
            _contentProvider = contentProvider;
            _passenger = passenger;
            _monthSelector = new MonthSelector(initialMonth);
            _monthCalendar = new CalendarMonth();
            _monthSelector.CurrentChanged += OnCurrentMonthChanged;
            ChangeThenFill();
        }

        public MonthSelector MonthSelector { get { return _monthSelector; } }

        public CalendarMonth MonthCalendar { get { return _monthCalendar; } }

        public string PassengerName { get { return _passenger.Name; } }

        private void OnCurrentMonthChanged(object sender, EventArgs e)
        {
            ChangeThenFill();
        }

        public void Refresh()
        {
            Fill();
        }

        private void ChangeThenFill()
        {
            var currentMonth = _monthSelector.Current;
            _monthCalendar.Change(currentMonth.Year, currentMonth.MonthInYear);
            Fill();
        }

        private void Fill()
        {
            var dayContentProvider = _contentProvider.GetContentProviderForDay(_passenger, MonthSelector.Current);
            _monthCalendar.Fill(dayContentProvider);
        }
    }
}
