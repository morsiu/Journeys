using System;

namespace Journeys.Commands
{
    public class AddJourneyCommand
    {
        public Guid JourneyId { get; private set; }

        public decimal JourneyDistance { get; private set; }

        public DateTime JourneyDateOfOccurrence { get; private set; }

        public string PersonName { get; private set; }

        public decimal LiftDistance { get; private set; }

        public AddJourneyCommand(
            Guid journeyId,
            decimal journeyDistance,
            DateTime journeyDateOfOccurrence,
            string personName,
            decimal liftDistance)
        {
            JourneyId = journeyId;
            JourneyDistance = journeyDistance;
            JourneyDateOfOccurrence = journeyDateOfOccurrence;
            PersonName = personName;
            LiftDistance = liftDistance;
        }
    }
}
