using System.Collections.Generic;
using System.Runtime.Serialization;
using Mors.Journeys.Data.Queries.Dtos;

namespace Mors.Journeys.Data.Queries
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
