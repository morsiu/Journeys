using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Data;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Application.Commands
{
    public class AddJourneyCommand
    {
        private int _journeyDistance;
        private DateTime _journeyDateOfOccurence;
        private Guid _personId;
        private int _liftDistance;

        public AddJourneyCommand(int journeyDistance, DateTime journeyDateOfOccurence, Guid PersonId, int liftDistance)
        {
            _journeyDistance = journeyDistance;
            _journeyDateOfOccurence = journeyDateOfOccurence;
            _personId = PersonId;
            _liftDistance = liftDistance;
        }

        internal void Execute(IEventBus eventBus, IJourneyRepository repository)
        {
            var journeyFactory = new JourneyFactory(eventBus);
            var journeyDistance = new Distance(_journeyDistance, DistanceUnit.Kilometer);
            var liftDistance = new Distance(_liftDistance, DistanceUnit.Kilometer);
            var personId = new Id<Person>(_personId);
            var journey = journeyFactory
                .Create(_journeyDateOfOccurence, journeyDistance)
                .AddLift(personId, liftDistance);
            repository.SaveJourney(journey);
        }
    }
}
