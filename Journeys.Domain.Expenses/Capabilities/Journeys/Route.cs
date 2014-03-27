using Journeys.Domain.Expenses.Capabilities.Journeys.Events;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Domain.Expenses.Capabilities.Journeys
{
    internal sealed class Route
    {
        private readonly IReadOnlyCollection<IJourneyEvent> _events;

        public Route(IReadOnlyCollection<IJourneyEvent> events)
        {
            _events = events.ToList();
        }

        public void Replay(IJourneyVisitor visitor)
        {
            IJourneyEvent previousEvent = null;
            using (var events = _events.GetEnumerator())
            {
                while (events.MoveNext())
                {
                    var currentEvent = events.Current;
                    DriveBetweenEvents(visitor, previousEvent, currentEvent);
                    currentEvent.Visit(visitor);
                    previousEvent = currentEvent;
                }
            }
        }

        private void DriveBetweenEvents(IJourneyVisitor visitor, IJourneyEvent previous, IJourneyEvent current)
        {
            if (previous == null) return;
            if (previous.Point < current.Point)
            {
                visitor.Visit(CreateDrive(previous, current));
            }
        }

        private Drive CreateDrive(IJourneyEvent last, IJourneyEvent current)
        {
            return new Drive(new RouteDistance(last.Point, current.Point));
        }
    }
}
