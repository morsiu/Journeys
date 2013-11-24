using Journeys.Common;
using Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Queries
{
    public class GetJourneysByPassengerThenMonthThenDayForPassengerAndDayQuery : IQuery<Fact>
    {
        public GetJourneysByPassengerThenMonthThenDayForPassengerAndDayQuery(IId passenger, DateTime day)
        {
            Day = day;
            PassengerId = passenger;
        }

        public IId PassengerId { get; private set; }

        public DateTime Day { get; private set; }
    }
}
