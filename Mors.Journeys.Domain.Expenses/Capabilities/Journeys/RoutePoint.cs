using System;
using Mors.Journeys.Domain.Infrastructure.Markers;

namespace Mors.Journeys.Domain.Expenses.Capabilities.Journeys
{
    [ValueObject]
    internal struct RoutePoint : IComparable<RoutePoint>
    {
        private readonly Distance _distanceFromStart;

        public RoutePoint(Distance distanceFromStart)
        {
            _distanceFromStart = distanceFromStart;
        }

        public Distance DistanceFromStart { get { return _distanceFromStart; } }

        public int CompareTo(RoutePoint other)
        {
            return _distanceFromStart.CompareTo(other._distanceFromStart);
        }

        public static bool operator==(RoutePoint a, RoutePoint b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(RoutePoint a, RoutePoint b)
        {
            return !a.Equals(b);
        }

        public static bool operator<(RoutePoint a, RoutePoint b)
        {
            return a.DistanceFromStart < b.DistanceFromStart;
        }

        public static bool operator>(RoutePoint a, RoutePoint b)
        {
            return a.DistanceFromStart > b.DistanceFromStart;
        }

        public static Distance operator-(RoutePoint a, RoutePoint b)
        {
            return a.DistanceFromStart - b.DistanceFromStart;
        }

        public bool Equals(RoutePoint other)
        {
            return _distanceFromStart == other._distanceFromStart;
        }

        public override bool Equals(object obj)
        {
            return obj is RoutePoint && Equals((RoutePoint)obj);
        }

        public override int GetHashCode()
        {
            return _distanceFromStart.GetHashCode();
        }
    }
}
