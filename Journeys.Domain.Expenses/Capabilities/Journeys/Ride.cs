using Journeys.Domain.Expenses.Capabilities.Journeys.Events;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Domain.Expenses.Capabilities.Journeys
{
    internal sealed class Ride
    {
        private readonly IReadOnlyCollection<IJourneyEvent> _events;

        public Ride(IReadOnlyCollection<IJourneyEvent> events)
        {
            _events = _events.ToList();
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
            if (IsDistanceBetween(previous, current))
            {
                visitor.Visit(CreateDrive(previous, current));
            }
        }

        private bool IsDistanceBetween(IJourneyEvent last, IJourneyEvent current)
        {
            return last.Distance < current.Distance;
        }

        private Drive CreateDrive(IJourneyEvent last, IJourneyEvent current)
        {
            return new Drive(new Distance(last.Distance, current.Distance));
        }
    }
}
