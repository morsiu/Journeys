using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Capabilities.RideEvents;
using Journeys.Domain.Infrastructure.Markers;
using System.Collections.Generic;

namespace Journeys.Domain.Expenses.Capabilities
{
    [Entity]
    public sealed class Journey
    {
        private readonly Ride _ride;
        private readonly IId _id;

        internal Journey(IId journeyId, Point journeyDistance, IEnumerable<Lift> lifts)
        {
            _id = journeyId;
            var events = CreateEvents(journeyDistance, lifts);
            _ride = new Ride(events);
        }

        public IId Id { get { return _id; } }

        private static IReadOnlyCollection<IRideEvent> CreateEvents(Point journeyDistance, IEnumerable<Lift> lifts)
        {
            var events = new List<IRideEvent>();
            events.Add(new RideStart());
            foreach (var lift in lifts)
            {
                events.Add(new PassengerPickup(lift.PassengerId, lift.Distance.From));
                events.Add(new PassengerExit(lift.PassengerId, lift.Distance.To));
            }
            events.Add(new RideFinish(journeyDistance));
            events.Sort((a, b) => a.Distance.CompareTo(b.Distance));
            return events;
        }

        internal void Visit(IJourneyVisitor visitor)
        {
            _ride.Replay(visitor);
        }
    }
}
