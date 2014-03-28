using Journeys.Queries.Dtos;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Journeys.Queries
{
    [DataContract]
    public class GetJourneysInPeriodQuery : IQuery<IEnumerable<Journey>>
    {
        public GetJourneysInPeriodQuery(Period period)
        {
            Period = period;
        }

        public Period Period { get; private set; }
    }
}
