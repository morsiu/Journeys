using System;
using Mors.Journeys.Domain.Infrastructure.Markers;

namespace Mors.Journeys.Domain.Expenses.Capabilities
{
    [ValueObject]
    public struct Distance : IComparable<Distance>
    {
        private readonly decimal _amount;

        public Distance(decimal amount)
        {
            _amount = amount;
        }

        public decimal Amount { get { return _amount; } }

        public static bool operator==(Distance left, Distance right)
        {
            return left.Equals(right);
        }

        public static bool operator!=(Distance left, Distance right)
        {
            return !(left == right);
        }

        public static bool operator<(Distance left, Distance right)
        {
            return left._amount < right._amount;
        }

        public static bool operator>(Distance left, Distance right)
        {
            return left._amount > right._amount;
        }

        public static Distance operator-(Distance a, Distance b)
        {
            return new Distance(Math.Abs(a.Amount - b.Amount));
        }

        public override bool Equals(object obj)
        {
            return obj is Distance && Equals((Distance)obj);
        }

        public override int GetHashCode()
        {
            return _amount.GetHashCode();
        }

        public bool Equals(Distance other)
        {
            return _amount == other._amount;
        }

        public int CompareTo(Distance distance)
        {
            return _amount.CompareTo(distance._amount);
        }
    }
}
