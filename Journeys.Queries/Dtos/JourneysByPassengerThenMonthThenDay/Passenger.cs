using Journeys.Common;
using System.Runtime.Serialization;

namespace Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    [DataContract]
    public struct Passenger
    {
        public Passenger(IId passengerId) : this()
        {
            Id = passengerId;
        }

        [DataMember]
        public IId Id { get; private set; }

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
