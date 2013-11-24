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
            _queryHandlerRegistry.SetHandler<GetPersonNameByIdQuery, string>(personView.Execute);
            _queryHandlerRegistry.SetHandler<GetPersonIdByNameQuery, IId>(personView.Execute);
            _queryHandlerRegistry.SetHandler<GetPeopleNamesQuery, IEnumerable<PersonName>>(personView.Execute);
            _eventBus.RegisterListener<PersonCreatedEvent>(personView.Update);

            var journeyView = new JourneysWithLiftsView(_queryDispatcher);
            _queryHandlerRegistry.SetHandler<GetJourneysWithLiftsByJourneyIdQuery, IEnumerable<JourneyWithLift>>(journeyView.Execute);
            _queryHandlerRegistry.SetHandler<GetJourneysWithLiftsQuery, IEnumerable<JourneyWithLift>>(journeyView.Execute);
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeyView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeyView.Update);

            var journeysByPassengerThenDayView = new JourneysByPassengerThenDayView();
            _queryHandlerRegistry.SetHandler<GetJourneysByDayForPassengerInPeriodQuery, IEnumerable<JourneysOnDay>>(journeysByPassengerThenDayView.Execute);
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeysByPassengerThenDayView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeysByPassengerThenDayView.Update);

            var journeysByPassengerThenMonthThenDayView = new JourneysByPassengerThenMonthThenDayView();
            _queryHandlerRegistry.SetHandler<GetJourneysByPassengerThenMonthThenDayQuery, IEnumerable<JourneysByPassengerThenMonthThenDay.Fact>>(journeysByPassengerThenMonthThenDayView.Execute);
            _queryHandlerRegistry.SetHandler<GetJourneysByPassengerThenMonthThenDayForPassengerAndDayQuery, JourneysByPassengerThenMonthThenDay.Fact>(journeysByPassengerThenMonthThenDayView.Execute);
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeysByPassengerThenMonthThenDayView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeysByPassengerThenMonthThenDayView.Update);
        }
    }
}
