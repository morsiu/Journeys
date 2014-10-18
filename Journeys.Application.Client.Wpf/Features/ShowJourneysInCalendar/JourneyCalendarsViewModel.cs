using System;
using System.Collections.ObjectModel;
using System.Linq;
using Journeys.Application.Client.Wpf.Events;
using Journeys.Data.Queries;

namespace Journeys.Application.Client.Wpf.Features.ShowJourneysInCalendar
{
    internal sealed class JourneyCalendarsViewModel
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IEventBus _eventBus;
        private readonly CalendarContentProvider _calendarContentProvider;
        private readonly ObservableCollection<PassengerLiftCalendar> _calendars;
        private readonly ReadOnlyObservableCollection<PassengerLiftCalendar> _calendarsWrapper;

        public JourneyCalendarsViewModel(IQueryDispatcher queryDispatcher, IEventBus eventBus)
        {
            _queryDispatcher = queryDispatcher;
            _eventBus = eventBus;
            _calendarContentProvider = new CalendarContentProvider(queryDispatcher);
            _calendars = new ObservableCollection<PassengerLiftCalendar>();
            _calendarsWrapper = new ReadOnlyObservableCollection<PassengerLiftCalendar>(_calendars);
            _eventBus.Subscribe<JourneyWithLiftsAddedEvent>(Handle);
        }

        private void Handle(JourneyWithLiftsAddedEvent @event)
        {
            _calendarContentProvider.Refresh();
            foreach (var calendar in _calendars)
            {
                calendar.Refresh();
            }
        }

        public ReadOnlyObservableCollection<PassengerLiftCalendar> Calendars { get { return _calendarsWrapper; } }

        public void Refresh()
        {
            var peopleNames = _queryDispatcher.Dispatch(new GetPeopleNamesQuery());
            var currentMonth = new Month(DateTime.Now.Year, DateTime.Now.Month);
            _calendars.Clear();
            _calendarContentProvider.Refresh();
            foreach (var calendar in peopleNames.Select(personName => new PassengerLiftCalendar(new Passenger(personName), currentMonth, _calendarContentProvider)))
            {
                _calendars.Add(calendar);
            }
        }
    }
}
