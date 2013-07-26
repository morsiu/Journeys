using Journeys.Queries.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
