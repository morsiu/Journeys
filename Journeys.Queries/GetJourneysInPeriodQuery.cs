using Journeys.Queries.Dtos;
using System.Collections.Generic;

namespace Journeys.Queries
{
    public class GetJourneysInPeriodQuery : IQuery<IEnumerable<Journey>>
    {
        public GetJourneysInPeriodQuery(Period period)
        {
            Period = period;
        }

        public Period Period { get; private set; }
    }
}
