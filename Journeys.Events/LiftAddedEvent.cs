using System;

namespace Journeys.Events
{
    public class LiftAddedEvent
    {
        public LiftAddedEvent(Guid journeyId, Guid personId, decimal liftDistance)
        {
            JourneyId = journeyId;
            PersonId = personId;
            LiftDistance = liftDistance;
        }

        public Guid JourneyId { get; private set; }

        public Guid PersonId { get; private set; }

        public decimal LiftDistance { get; private set; }
    }
}
