using System;
using System.Collections.Generic;
using Journeys.Queries.Dtos;

namespace Journeys.Queries
{
    public class GetJourneysByDayForPassengerInPeriodQuery : IQuery<IEnumerable<JourneysOnDay>>
    {
        public GetJourneysByDayForPassengerInPeriodQuery(Guid passengerId, DateTime periodStart, DateTime periodEnd)
        {
            PassengerId = passengerId;
            PeriodStart = periodStart;
            PeriodEnd = periodEnd;
        }

        public Guid PassengerId { get; private set; }

        public DateTime PeriodStart { get; private set; }

        public DateTime PeriodEnd { get; private set; }
    }
}