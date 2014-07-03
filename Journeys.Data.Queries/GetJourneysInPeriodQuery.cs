using Journeys.Data.Queries.Dtos;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Journeys.Data.Queries
{
    [DataContract]
    public class GetJourneysInPeriodQuery : IQuery<IEnumerable<Journey>>
    {
        public GetJourneysInPeriodQuery(Period period)
        {
            Period = period;
        }

        [DataMember]
        public Period Period { get; private set; }
    }
}
