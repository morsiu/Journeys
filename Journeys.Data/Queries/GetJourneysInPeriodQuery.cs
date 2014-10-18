using System.Collections.Generic;
using System.Runtime.Serialization;
using Journeys.Data.Queries.Dtos;

namespace Journeys.Data.Queries
{
    [DataContract]
    public sealed class GetJourneysInPeriodQuery : IQuery<IEnumerable<Journey>>
    {
        public GetJourneysInPeriodQuery(Period period)
        {
            Period = period;
        }

        [DataMember]
        public Period Period { get; private set; }
    }
}
