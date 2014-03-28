using System.Runtime.Serialization;

namespace Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    [DataContract]
    public struct Key
    {
        public Key(Passenger passenger, Month month, Day day) : this()
        {
            Passenger = passenger;
            Month = month;
            Day = day;
        }

        [DataMember]
        public Passenger Passenger { get; private set; }

        [DataMember]
        public Month Month { get; private set; }

        [DataMember]
        public Day Day { get; private set; }
    }
}
