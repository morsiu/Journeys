using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    public struct Month
    {
        public Month(int year, int monthOfYear) : this()
        {
            Year = year;
            MonthOfYear = monthOfYear;
        }

        public int MonthOfYear { get; private set; }

        public int Year { get; private set; }
    }
}
