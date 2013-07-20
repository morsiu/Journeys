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
        private readonly Guid _journeyId;
        private readonly int _journeyDistance;
        private readonly DateTime _journeyDateOfOccurence;
        private readonly Guid _personId;
        private readonly int _liftDistance;

        public AddJourneyCommand(Guid journeyId, int journeyDistance, DateTime journeyDateOfOccurence, Guid personId, int liftDistance)
        {
            _journeyId = journeyId;
            _journeyDistance = journeyDistance;
            _journeyDateOfOccurence = journeyDateOfOccurence;
            _personId = personId;
            _liftDistance = liftDistance;
        }

        internal void Execute(IEventBus eventBus, IDomainRepository<Journey> repository)
        {
            var journeyDistance = new Distance(_journeyDistance, DistanceUnit.Kilometer);
            var liftDistance = new Distance(_liftDistance, DistanceUnit.Kilometer);
            var personId = new Id<Person>(_personId);
            var journeyId = new Id<Journey>(_journeyId);
            var journey = new Journey(journeyId, _journeyDateOfOccurence, journeyDistance, eventBus)
                .AddLift(personId, liftDistance);
            repository.Store(journey);
        }
    }
}
