using System.Collections.Generic;
using Mors.Journeys.Domain.Expenses.Capabilities.Journeys;
using Mors.Journeys.Domain.Expenses.Capabilities.Journeys.Events;
using Mors.Journeys.Domain.Infrastructure.Markers;

namespace Mors.Journeys.Domain.Expenses.Capabilities
{
    [Entity]
    public sealed class Journey
    {
        private readonly Route _route;
        private readonly object _id;

        internal Journey(object journeyId, Distance routeDistance, IEnumerable<Lift> journeyLifts)
        {
            _id = journeyId;
            var events = CreateEvents(routeDistance, journeyLifts);
            _route = new Route(events);
        }

        public object Id { get { return _id; } }

        private static IReadOnlyCollection<IJourneyEvent> CreateEvents(Distance routeDistance, IEnumerable<Lift> lifts)
        {
            var events = new List<IJourneyEvent>();
            events.Add(new JourneyStart());
            foreach (var lift in lifts)
            {
                events.Add(new PassengerPickup(lift.PassengerId, lift.Distance.From));
                events.Add(new PassengerExit(lift.PassengerId, lift.Distance.To));
            }
            events.Add(new JourneyFinish(new RoutePoint(routeDistance)));
            events.Sort((a, b) => a.Point.CompareTo(b.Point));
            return events;
        }

        internal void Visit(IJourneyVisitor visitor)
        {
            _route.Replay(visitor);
        }
    }
}
