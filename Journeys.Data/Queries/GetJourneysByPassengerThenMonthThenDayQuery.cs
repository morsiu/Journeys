using Journeys.Data.Queries.Dtos.JourneysByPassengerThenMonthThenDay;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Journeys.Data.Queries
{
    [DataContract]
    public sealed class GetJourneysByPassengerThenMonthThenDayQuery : IQuery<IEnumerable<Fact>>
    {
    }
}
