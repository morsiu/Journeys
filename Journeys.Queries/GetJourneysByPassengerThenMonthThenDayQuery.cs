using Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Journeys.Queries
{
    [DataContract]
    public class GetJourneysByPassengerThenMonthThenDayQuery : IQuery<IEnumerable<Fact>>
    {
    }
}
