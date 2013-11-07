using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    public struct Day
    {
        public Day(int dayOfMonth) : this()
        {
            DayOfMonth = dayOfMonth;
        }

        public int DayOfMonth { get; private set; }
    }
}
