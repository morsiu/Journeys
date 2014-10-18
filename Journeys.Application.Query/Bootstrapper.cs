using System.Collections.Generic;
using Journeys.Data.Events;
using Journeys.Data.Queries;
using Journeys.Data.Queries.Dtos;

namespace Journeys.Application.Query
{
    public sealed class Bootstrapper
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
            _queryHandlerRegistry.SetHandler<GetPersonIdByNameQuery, object>(personView.Execute);
            _queryHandlerRegistry.SetHandler<GetPeopleNamesQuery, IEnumerable<PersonName>>(personView.Execute);
            _eventBus.RegisterListener<PersonCreatedEvent>(personView.Update);

            var journeysByPassengerThenMonthThenDayView = new JourneysByPassengerThenMonthThenDayView();
            _queryHandlerRegistry.SetHandler<GetJourneysByPassengerThenMonthThenDayQuery, IEnumerable<Data.Queries.Dtos.JourneysByPassengerThenMonthThenDay.Fact>>(journeysByPassengerThenMonthThenDayView.Execute);
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeysByPassengerThenMonthThenDayView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeysByPassengerThenMonthThenDayView.Update);

            var journeyView = new JourneyView();
            _queryHandlerRegistry.SetHandler<GetJourneysInPeriodQuery, IEnumerable<Journey>>(journeyView.Execute);
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeyView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeyView.Update);

            var passengerLiftsCostCalculator = new PassengerLiftExpensesCalculator(_queryDispatcher);
            _queryHandlerRegistry.SetHandler<GetCostOfPassengerLiftsInPeriodQuery, PassengerLiftsCost>(passengerLiftsCostCalculator.Execute);
        }
    }
}
