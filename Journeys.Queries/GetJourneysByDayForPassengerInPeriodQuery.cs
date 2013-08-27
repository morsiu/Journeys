using System;
using System.Collections.Generic;
using Journeys.Common;
using Journeys.Queries.Dtos;

namespace Journeys.Queries
{
    public class GetJourneysByDayForPassengerInPeriodQuery : IQuery<IEnumerable<JourneysOnDay>>
    {
        public GetJourneysByDayForPassengerInPeriodQuery(IId passengerId, DateTime periodStart, DateTime periodEnd)
        {
            PassengerId = passengerId;
            PeriodStart = periodStart;
            PeriodEnd = periodEnd;
        }

        public IId PassengerId { get; private set; }

        public DateTime PeriodStart { get; private set; }

        public DateTime PeriodEnd { get; private set; }
    }
}