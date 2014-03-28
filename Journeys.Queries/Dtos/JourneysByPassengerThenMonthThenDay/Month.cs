using System.Runtime.Serialization;

namespace Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    [DataContract]
    public struct Month
    {
        public Month(int year, int monthOfYear) : this()
        {
            Year = year;
            MonthOfYear = monthOfYear;
        }

        [DataMember]
        public int MonthOfYear { get; private set; }

        [DataMember]
        public int Year { get; private set; }
    }
}
