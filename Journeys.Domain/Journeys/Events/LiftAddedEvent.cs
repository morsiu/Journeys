using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;

namespace Journeys.Domain.Journeys.Events
{
    public class LiftAddedEvent
    {
        public LiftAddedEvent(Id journeyId, Id personId, Distance liftDistance)
        {
            JourneyId = journeyId;
            PersonId = personId;
            LiftDistance = liftDistance;
        }

        public Id JourneyId { get; private set; }

        public Id PersonId { get; private set; }

        public Distance LiftDistance { get; private set; }
    }
}
