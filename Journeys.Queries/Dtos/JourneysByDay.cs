using System;

namespace Journeys.Queries.Dtos
{
    public class JourneysByDay
    {
        public JourneysByDay(DateTime date, int journeyCount, decimal totalJourneysDistance, decimal totalLiftDistance)
        {
            Date = date;
            JourneyCount = journeyCount;
            TotalJourneysDistance = totalJourneysDistance;
            TotalLiftDistance = totalLiftDistance;
        }

        public DateTime Date { get; private set; }

        public int JourneyCount { get; private set; }

        public decimal TotalJourneysDistance { get; private set; }

        public decimal TotalLiftDistance { get; private set; }
    }
}