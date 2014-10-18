using System.Collections.Generic;
using System.Runtime.Serialization;
using Mors.AppPlatform.Common;
using Mors.Journeys.Data.Queries.Dtos.JourneysByPassengerThenMonthThenDay;

namespace Mors.Journeys.Data.Queries
{
    [DataContract]
    public sealed class GetJourneysByPassengerThenMonthThenDayQuery : IQuery<IEnumerable<Fact>>
    {
    }
}
