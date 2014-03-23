using System.Collections.Generic;
using Journeys.Common;
using Journeys.Events;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using JourneysByPassengerThenMonthThenDay = Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay;

namespace Journeys.Query
{
    public class Bootstrapper
    {
        private readonly IEventBus _eventBus;
        private readonly IQueryHandlerRegistry _queryHandlerRegistry;
        private readonly IQueryDispatcher _queryDispatcher;

        public Bootstrapper(IEventBus eventBus, IQueryDispatcher queryDispatcher, IQueryHandlerRegistry queryHandlerRegistry)
        {
            _eventBus = eventBus;
            _queryDispatcher = queryDispatcher;
            _queryHandlerRegistry = queryHandlerRegistry;
        }

        public void Bootstrap()
        {
            var personView = new PersonView();
            _queryHandlerRegistry.SetHandler<GetPersonIdByNameQuery, IId>(personView.Execute);
            _queryHandlerRegistry.SetHandler<GetPeopleNamesQuery, IEnumerable<PersonName>>(personView.Execute);
            _eventBus.RegisterListener<PersonCreatedEvent>(personView.Update);

            var journeysByPassengerThenMonthThenDayView = new JourneysByPassengerThenMonthThenDayView();
            _queryHandlerRegistry.SetHandler<GetJourneysByPassengerThenMonthThenDayQuery, IEnumerable<JourneysByPassengerThenMonthThenDay.Fact>>(journeysByPassengerThenMonthThenDayView.Execute);
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeysByPassengerThenMonthThenDayView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeysByPassengerThenMonthThenDayView.Update);

            var journeyView = new JourneyView();
            _queryHandlerRegistry.SetHandler<GetJourneysInPeriodQuery, IEnumerable<Journey>>(journeyView.Execute);
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeyView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeyView.Update);
        }
    }
}
