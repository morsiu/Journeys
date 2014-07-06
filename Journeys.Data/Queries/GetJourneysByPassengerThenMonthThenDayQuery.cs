using Journeys.Data.Queries.Dtos.JourneysByPassengerThenMonthThenDay;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Journeys.Data.Queries
{
    [DataContract]
    public class GetJourneysByPassengerThenMonthThenDayQuery : IQuery<IEnumerable<Fact>>
    {
    }
}
