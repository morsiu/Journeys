using Journeys.Commands;
using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Data;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using Journeys.Eventing;
using System;

namespace Journeys.Application.CommandHandlers
{
    internal static class AddJourneyCommandHandler
    {
        public static void Execute(AddJourneyCommand command, IEventBus eventBus, IDomainRepository<Journey> repository)
        {
            var journeyDistance = new Distance(command.JourneyDistance, DistanceUnit.Kilometer);
            var liftDistance = new Distance(command.LiftDistance, DistanceUnit.Kilometer);
            var personId = new Id(command.PersonId);
            var journeyId = new Id(command.JourneyId);
            var journey = new Journey(journeyId, command.JourneyDateOfOccurence, journeyDistance, eventBus)
                .AddLift(personId, liftDistance);
            repository.Store(journey);
        }
    }
}
