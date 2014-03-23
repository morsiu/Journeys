using Journeys.Domain.Infrastructure.Markers;
using System;

namespace Journeys.Domain.Expenses.Capabilities
{
    [ValueObject]
    internal struct Point : IComparable<Point>, IEquatable<Point>
    {
        private readonly decimal _distance;

        public Point(decimal distance)
        {
            _distance = distance;
        }

        public decimal Distance { get { return _distance; } }

        public int CompareTo(Point other)
        {
            return _distance.CompareTo(other._distance);
        }

        public static bool operator==(Point a, Point b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(Point a, Point b)
        {
            return !a.Equals(b);
        }

        public static bool operator<(Point a, Point b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator>(Point a, Point b)
        {
            return a.CompareTo(b) > 0;
        }

        public bool Equals(Point other)
        {
            return _distance == other._distance;
        }

        public override bool Equals(object obj)
        {
            return obj is Point && Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            return _distance.GetHashCode();
        }
    }
}
