using System;

namespace Journeys.Queries.Dtos
{
    public class JourneysOnDay
    {
        public JourneysOnDay(DateTime day, int journeyCount, decimal totalRouteDistance, decimal totalLiftDistance)
        {
            Day = day;
            JourneyCount = journeyCount;
            TotalRouteDistance = totalRouteDistance;
            TotalLiftDistance = totalLiftDistance;
        }

        public DateTime Day { get; private set; }

        public int JourneyCount { get; private set; }

        public decimal TotalRouteDistance { get; private set; }

        public decimal TotalLiftDistance { get; private set; }
    }
}