using Journeys.Domain.Markers;
using Journeys.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Journeys.Capabilities
{
    [ValueObject]
    public struct Distance : IComparable<Distance>
    {
        private decimal _amount;
        private DistanceUnit _unit;

        public Distance(decimal amount, DistanceUnit unit)
        {
            if (amount < 0m) throw new ArgumentException(FailureMessages.DistanceAmountMustNotBeNegative, "amount");
            _amount = amount;
            _unit = unit;
        }

        public int CompareTo(Distance other)
        {
            return _amount.CompareTo(other._amount);
        }
    }
}
