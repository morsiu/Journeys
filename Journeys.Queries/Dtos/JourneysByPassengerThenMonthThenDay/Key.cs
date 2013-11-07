using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    public struct Key
    {
        public Key(Passenger passenger, Month month, Day day) : this()
        {
            Passenger = passenger;
            Month = month;
            Day = day;
        }

        public Passenger Passenger { get; private set; }

        public Month Month { get; private set; }

        public Day Day { get; private set; }
    }
}
