using Journeys.Domain.Infrastructure.Markers;
using Journeys.Domain.Infrastructure.Messages;
using System;

namespace Journeys.Domain.Journeys.Capabilities
{
    [ValueObject]
    public struct Distance
    {
        private readonly decimal _amount;
        private readonly DistanceUnit _unit;

        public Distance(decimal amount, DistanceUnit unit)
        {
            if (amount < 0m) throw new ArgumentException(FailureMessages.DistanceAmountMustNotBeNegative, "amount");
            _amount = amount;
            _unit = unit;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Distance == false) return false;
            return Equals((Distance)obj);
        }

        public override int GetHashCode()
        {
            return _amount.GetHashCode() * 37 + _unit.GetHashCode();
        }

        public bool Equals(Distance other)
        {
            return _amount == other._amount;
        }

        public static bool operator==(Distance a, Distance b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(Distance a, Distance b)
        {
            return !a.Equals(b);
        }

        public static bool operator<(Distance a, Distance b)
        {
            return a._amount < b._amount;
        }

        public static bool operator>(Distance a, Distance b)
        {
            return a._amount > b._amount;
        }
    }
}
