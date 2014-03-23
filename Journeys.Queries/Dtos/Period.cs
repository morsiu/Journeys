using System;

namespace Journeys.Queries.Dtos
{
    public class Period
    {
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }
    }
}
