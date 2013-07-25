using Journeys.Commands;
using Journeys.Data;
using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Data;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using Journeys.Eventing;
using Journeys.Queries;
using System;

namespace Journeys.Application.CommandHandlers
{
    internal static class AddJourneyCommandHandler
    {
        public static void Execute(
            AddJourneyCommand command,
            IEventBus eventBus,
            IDomainRepository<Journey> journeyRepository,
            IDomainRepository<Person> personRepository,
            IQueryDispatcher queryDispatcher)
        {
            var journeyDistance = new Distance(command.JourneyDistance, DistanceUnit.Kilometer);
            var liftDistance = new Distance(command.LiftDistance, DistanceUnit.Kilometer);
            var personId = queryDispatcher.Dispatch(new GetIdOfPersonWithNameQuery(command.PersonName));
            if (!personId.HasValue)
            {
                personId = Guid.NewGuid();
                var person = new Person(new Id<Person>(personId.Value), command.PersonName, eventBus);
                personRepository.Store(person);
            }
            var journeyId = new Id<Journey>(command.JourneyId);
            var journey = new Journey(journeyId, command.JourneyDateOfOccurence, journeyDistance, eventBus)
                .AddLift(new Id<Person>(personId.Value), liftDistance);
            journeyRepository.Store(journey);
        }
    }
}
