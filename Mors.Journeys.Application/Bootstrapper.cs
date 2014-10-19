using System.Collections.Generic;
using Mors.AppPlatform.Common.Services;
using Mors.Journeys.Application.CommandHandlers;
using Mors.Journeys.Application.EventReplayers;
using Mors.Journeys.Application.QueryHandlers;
using Mors.Journeys.Data.Commands;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Data.Queries;
using Mors.Journeys.Data.Queries.Dtos;
using Mors.Journeys.Data.Queries.Dtos.JourneysByPassengerThenMonthThenDay;

namespace Mors.Journeys.Application
{
    public sealed class Bootstrapper
    {
        private readonly IEventBus _eventBus;
        private readonly IRepositories _repositories;
        private readonly IIdFactory _idFactory;
        private readonly ICommandHandlerRegistry _commandHandlerRegistry;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IQueryHandlerRegistry _queryHandlerRegistry;
        private readonly IEventSourcing _eventSourcing;

        public Bootstrapper(
            IEventBus eventBus,
            IRepositories repositories,
            IIdFactory idFactory,
            ICommandHandlerRegistry commandHandlerRegistry,
            IQueryDispatcher queryDispatcher,
            IQueryHandlerRegistry queryHandlerRegistry,
            IEventSourcing eventSourcing)
        {
            _eventBus = eventBus;
            _idFactory = idFactory;
            _repositories = repositories;
            _commandHandlerRegistry = commandHandlerRegistry;
            _queryDispatcher = queryDispatcher;
            _queryHandlerRegistry = queryHandlerRegistry;
            _eventSourcing = eventSourcing;
        }

        public void Bootstrap()
        {
            BootstrapEventSourcing();
            BootstrapQueries();
            BootstrapCommands();
        }

        private void BootstrapCommands()
        {
            _commandHandlerRegistry.SetHandler<AddJourneyWithLiftsCommand>(
                new AddJourneyWithLiftsCommandHandler(_eventBus, _repositories, _idFactory, _queryDispatcher).ExecuteTransacted);
        }

        private void BootstrapEventSourcing()
        {
            _eventSourcing.RegisterEventReplayer<JourneyCreatedEvent>(new JourneyCreatedEventReplayer(_repositories, _eventBus).Replay);
            _eventSourcing.RegisterEventReplayer<LiftAddedEvent>(new LiftAddedEventReplayer(_repositories).Replay);
            _eventSourcing.RegisterEventReplayer<PersonCreatedEvent>(new PersonCreatedEventReplayer(_repositories, _eventBus).Replay);
        }

        public void BootstrapQueries()
        {
            var personView = new PersonView();
            _queryHandlerRegistry.SetHandler<GetPersonIdByNameQuery, object>(personView.Execute);
            _queryHandlerRegistry.SetHandler<GetPeopleNamesQuery, IEnumerable<PersonName>>(personView.Execute);
            _eventBus.RegisterListener<PersonCreatedEvent>(personView.Update);

            var journeysByPassengerThenMonthThenDayView = new JourneysByPassengerThenMonthThenDayView();
            _queryHandlerRegistry.SetHandler<GetJourneysByPassengerThenMonthThenDayQuery, IEnumerable<Fact>>(journeysByPassengerThenMonthThenDayView.Execute);
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeysByPassengerThenMonthThenDayView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeysByPassengerThenMonthThenDayView.Update);

            var journeyView = new JourneyView();
            _queryHandlerRegistry.SetHandler<GetJourneysInPeriodQuery, IEnumerable<Journey>>(journeyView.Execute);
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeyView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeyView.Update);

            var passengerLiftsCostCalculator = new PassengerLiftCostCalculator(_queryDispatcher);
            _queryHandlerRegistry.SetHandler<GetCostOfPassengerLiftsInPeriodQuery, PassengerLiftsCost>(passengerLiftsCostCalculator.Execute);
        }
    }
}
