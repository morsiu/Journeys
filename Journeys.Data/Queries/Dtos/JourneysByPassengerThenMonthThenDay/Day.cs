using System.Runtime.Serialization;

namespace Journeys.Data.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    [DataContract]
    public struct Day
    {
        public Day(int dayOfMonth) : this()
        {
            DayOfMonth = dayOfMonth;
        }

        [DataMember]
        public int DayOfMonth { get; private set; }
    }
}
