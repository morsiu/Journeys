using Journeys.Events;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Query
{
    internal class JourneyView
    {
        public void Update(JourneyCreatedEvent obj)
        {
        }

        public void Update(LiftAddedEvent obj)
        {
        }

        public IEnumerable<Journey> Execute(GetJourneysInPeriodQuery query)
        {
            return Enumerable.Empty<Journey>();
        }
    }
}
