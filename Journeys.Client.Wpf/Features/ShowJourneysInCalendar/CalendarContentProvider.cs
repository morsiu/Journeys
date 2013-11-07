using Journeys.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using Data = Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay;

namespace Journeys.Client.Wpf.Features.ShowJourneysInCalendar
{
    internal class CalendarContentProvider
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly Dictionary<Data.Key, JourneyDaySummary> _contents;

        public CalendarContentProvider(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _contents = new Dictionary<Data.Key, JourneyDaySummary>();
        }

        public void Refresh()
        {
            var facts = _queryDispatcher.Dispatch(new GetJourneysByPassengerThenMonthThenDayQuery());
            foreach (var fact in facts)
            {
                JourneyDaySummary content;
                if (_contents.TryGetValue(fact.Key, out content))
                {
                    content.Change(fact.Value);
                }
                else
                {
                    _contents[fact.Key] = new JourneyDaySummary(fact.Value);
                }
            }
        }

        public Func<int, object> GetContentProviderForDay(Passenger passenger, Month month)
        {
            return dayOfMonth => _contents
                .Where(fact =>
                    fact.Key.Month.Year == month.Year &&
                    fact.Key.Month.MonthOfYear == month.MonthInYear &&
                    fact.Key.Passenger.Id == passenger.Id &&
                    fact.Key.Day.DayOfMonth == dayOfMonth)
                .Select(fact => fact.Value);
        }
    }
}
