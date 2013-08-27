using System.Collections.Generic;
using Journeys.Common;
using Journeys.Queries.Dtos;

namespace Journeys.Queries
{
    public class GetJourneysWithLiftsByJourneyIdQuery : IQuery<IEnumerable<JourneyWithLift>>
    {
        public IId JourneyId { get; private set; }

        public GetJourneysWithLiftsByJourneyIdQuery(IId journeyId)
        {
            JourneyId = journeyId;
        }
    }
}
