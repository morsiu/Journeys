using System;
using System.Collections.Generic;
using Journeys.Eventing;
using Journeys.Events;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using Journeys.Query.Infrastructure;

namespace Journeys.Query
{
    public class Bootstrapper
    {
        private readonly EventBus _eventBus;

        public Bootstrapper(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IQueryDispatcher QueryDispatcher { get; private set; }

        public void Bootstrap()
        {
            var queryProcessor = new QueryProcessor();
            var queryDispatcher = new QueryDispatcher(queryProcessor);

            var personView = new PersonView();
            queryProcessor.SetHandler<GetPersonNameByIdQuery, string>(personView.Execute);
            queryProcessor.SetHandler<GetPersonIdByNameQuery, Guid?>(personView.Execute);
            queryProcessor.SetHandler<GetPeopleNamesQuery, IEnumerable<PersonName>>(personView.Execute);
            _eventBus.RegisterListener<PersonCreatedEvent>(personView.Update);

            var journeyView = new JourneysWithLiftsView(queryDispatcher);
            queryProcessor.SetHandler<GetJourneysWithLiftsByJourneyIdQuery, IEnumerable<JourneyWithLift>>(journeyView.Execute);
            queryProcessor.SetHandler<GetJourneysWithLiftsQuery, IEnumerable<JourneyWithLift>>(journeyView.Execute);
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeyView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeyView.Update);

            var journeysByPassengerThenDayView = new JourneysByPassengerThenDayView();
            queryProcessor.SetHandler<GetJourneysByDayForPassengerInPeriodQuery, IEnumerable<JourneysOnDay>>(journeysByPassengerThenDayView.Execute);
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeysByPassengerThenDayView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeysByPassengerThenDayView.Update);

            QueryDispatcher = new QueryDispatcher(queryProcessor);
        }
    }
}
