using System.Collections.Generic;
using System.Runtime.Serialization;
using Journeys.Data.Queries.Dtos.JourneysByPassengerThenMonthThenDay;

namespace Journeys.Data.Queries
{
    [DataContract]
    public sealed class GetJourneysByPassengerThenMonthThenDayQuery : IQuery<IEnumerable<Fact>>
    {
    }
}
