using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Journeys.Events
{
    public class LiftAddedEvent
    {
        public LiftAddedEvent(Id<Journey> journeyId, Id<Person> personId, Distance liftDistance)
        {
            JourneyId = journeyId;
            PersonId = personId;
            LiftDistance = liftDistance;
        }

        public Id<Journey> JourneyId { get; private set; }

        public Id<Person> PersonId { get; private set; }

        public Distance LiftDistance { get; private set; }
    }
}
