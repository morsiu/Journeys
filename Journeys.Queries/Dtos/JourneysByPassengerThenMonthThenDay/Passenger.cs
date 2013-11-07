using Journeys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Queries.Dtos.JourneysByPassengerThenMonthThenDay
{
    public struct Passenger
    {
        public Passenger(IId passengerId) : this()
        {
            Id = passengerId;
        }

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
    }
}
