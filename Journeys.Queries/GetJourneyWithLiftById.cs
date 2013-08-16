using System;
using System.Collections.Generic;
using Journeys.Queries.Dtos;

namespace Journeys.Queries
{
    public class GetJourneysWithLiftsByJourneyIdQuery : IQuery<IEnumerable<JourneyWithLift>>
    {
        public Guid JourneyId { get; set; }

        public GetJourneysWithLiftsByJourneyIdQuery(Guid journeyId)
        {
            JourneyId = journeyId;
        }
    }
}
