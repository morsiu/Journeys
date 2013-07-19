using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Data;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using System;

namespace Journeys.Application.Commands
{
    public class AddJourneyCommand
    {
        private readonly int _journeyDistance;
        private readonly DateTime _journeyDateOfOccurence;
        private readonly Guid _personId;
        private readonly int _liftDistance;

        public AddJourneyCommand(int journeyDistance, DateTime journeyDateOfOccurence, Guid PersonId, int liftDistance)
        {
            _journeyDistance = journeyDistance;
            _journeyDateOfOccurence = journeyDateOfOccurence;
            _personId = PersonId;
            _liftDistance = liftDistance;
        }

        internal void Execute(IEventBus eventBus, IDomainRepository<Journey> repository)
        {
            var journeyFactory = new JourneyFactory(eventBus);
            var journeyDistance = new Distance(_journeyDistance, DistanceUnit.Kilometer);
            var liftDistance = new Distance(_liftDistance, DistanceUnit.Kilometer);
            var personId = new Id<Person>(_personId);
            var journey = journeyFactory
                .Create(_journeyDateOfOccurence, journeyDistance)
                .AddLift(personId, liftDistance);
            repository.Store(journey);
        }
    }
}
