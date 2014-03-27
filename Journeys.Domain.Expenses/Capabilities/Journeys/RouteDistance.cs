using Journeys.Domain.Infrastructure.Markers;
using System;

namespace Journeys.Domain.Expenses.Capabilities
{
    [ValueObject]
    internal struct RouteDistance
    {
        private readonly RoutePoint _from;
        private readonly RoutePoint _to;

        public RouteDistance(RoutePoint from, RoutePoint to)
        {
            _from = from;
            _to = to;
        }

        public RoutePoint From { get { return _from; } }

        public RoutePoint To { get { return _to; } }

        public Distance Length { get { return _to - _from; } }
    }
}
