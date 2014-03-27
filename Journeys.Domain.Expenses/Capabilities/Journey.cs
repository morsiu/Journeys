using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Capabilities.Journeys;
using Journeys.Domain.Expenses.Capabilities.Journeys.Events;
using Journeys.Domain.Infrastructure.Markers;
using System.Collections.Generic;

namespace Journeys.Domain.Expenses.Capabilities
{
    [Entity]
    public sealed class Journey
    {
        private readonly Route _ride;
        private readonly IId _id;

        internal Journey(IId journeyId, Point journeyDistance, IEnumerable<Lift> lifts)
        {
            _id = journeyId;
            var events = CreateEvents(journeyDistance, lifts);
            _ride = new Route(events);
        }

        public IId Id { get { return _id; } }

        private static IReadOnlyCollection<IJourneyEvent> CreateEvents(Point journeyDistance, IEnumerable<Lift> lifts)
        {
            var events = new List<IJourneyEvent>();
            events.Add(new JourneyStart());
            foreach (var lift in lifts)
            {
                events.Add(new PassengerPickup(lift.PassengerId, lift.Distance.From));
                events.Add(new PassengerExit(lift.PassengerId, lift.Distance.To));
            }
            events.Add(new JourneyFinish(journeyDistance));
            events.Sort((a, b) => a.Distance.CompareTo(b.Distance));
            return events;
        }

        internal void Visit(IJourneyVisitor visitor)
        {
            _ride.Replay(visitor);
        }
    }
}
