using System.Runtime.Serialization;

namespace Journeys.Data.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    [DataContract]
    public struct Passenger
    {
        public Passenger(object passengerId) : this()
        {
            Id = passengerId;
        }

        [DataMember]
        public object Id { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is Passenger
                && Equals(((Passenger)obj).Id, Id);
        }

        public override int GetHashCode()
        {
            return Id == null ? 0 : Id.GetHashCode();
        }

        public static bool operator ==(Passenger a, Passenger b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Passenger a, Passenger b)
        {
            return !a.Equals(b);
        }
    }
}
