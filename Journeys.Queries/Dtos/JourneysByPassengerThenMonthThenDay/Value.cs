using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    public struct Value
    {
        public Value(int journeyCount, decimal journeyDistance, int liftCount, decimal liftDistance) : this()
        {
            JourneyCount = journeyCount;
            JourneyDistance = journeyDistance;
            LiftCount = liftCount;
            LiftDistance = liftDistance;
        }

        public decimal LiftDistance { get; private set; }

        public int LiftCount { get; private set; }

        public decimal JourneyDistance { get; private set; }

        public int JourneyCount { get; private set; }
    }
}
