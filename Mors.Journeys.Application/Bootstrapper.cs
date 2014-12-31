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
        public void BootstrapCommands(
            ICommandHandlerRegistry commandHandlerRegistry,
            IRepositories repositories,
            IEventBus eventBus,
            IIdFactory idFactory,
            IQueryDispatcher queryDispatcher)
        {
            commandHandlerRegistry.SetHandler<AddJourneyWithLiftsCommand>(
                new AddJourneyWithLiftsCommandHandler(eventBus, repositories, idFactory, queryDispatcher).Execute);
        }

        public void BootstrapEventSourcing(
            IEventSourcing eventSourcing,
            IRepositories repositories,
            IEventBus eventBus)
        {
            eventSourcing.RegisterEventReplayer<JourneyCreatedEvent>(new JourneyCreatedEventReplayer(repositories, eventBus).Replay);
            eventSourcing.RegisterEventReplayer<LiftAddedEvent>(new LiftAddedEventReplayer(repositories).Replay);
            eventSourcing.RegisterEventReplayer<PersonCreatedEvent>(new PersonCreatedEventReplayer(repositories, eventBus).Replay);
        }

        public void BootstrapQueries(
            IQueryHandlerRegistry queryHandlerRegistry,
            IEventBus eventBus,
            IQueryDispatcher queryDispatcher)
        {
            var personView = new PersonView();
            queryHandlerRegistry.SetHandler<GetPersonIdByNameQuery, object>(personView.Execute);
            queryHandlerRegistry.SetHandler<GetPeopleNamesQuery, IEnumerable<PersonName>>(personView.Execute);
            eventBus.RegisterListener<PersonCreatedEvent>(personView.Update);

            var journeysByPassengerThenMonthThenDayView = new JourneysByPassengerThenMonthThenDayView();
            queryHandlerRegistry.SetHandler<GetJourneysByPassengerThenMonthThenDayQuery, IEnumerable<Fact>>(journeysByPassengerThenMonthThenDayView.Execute);
            eventBus.RegisterListener<JourneyCreatedEvent>(journeysByPassengerThenMonthThenDayView.Update);
            eventBus.RegisterListener<LiftAddedEvent>(journeysByPassengerThenMonthThenDayView.Update);

            var journeyView = new JourneyView();
            queryHandlerRegistry.SetHandler<GetJourneysInPeriodQuery, IEnumerable<Journey>>(journeyView.Execute);
            eventBus.RegisterListener<JourneyCreatedEvent>(journeyView.Update);
            eventBus.RegisterListener<LiftAddedEvent>(journeyView.Update);

            var passengerLiftsCostCalculator = new PassengerLiftCostCalculator(queryDispatcher);
            queryHandlerRegistry.SetHandler<GetCostOfPassengerLiftsInPeriodQuery, PassengerLiftsCost>(passengerLiftsCostCalculator.Execute);
        }
    }
}
