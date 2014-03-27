using Journeys.Domain.Expenses.Capabilities.RideEvents;
using System.Collections.Generic;
using System.Linq;

namespace Journeys.Domain.Expenses.Capabilities
{
    internal sealed class Ride
    {
        private readonly IReadOnlyCollection<IRideEvent> _events;

        public Ride(IReadOnlyCollection<IRideEvent> events)
        {
            _events = _events.ToList();
        }

        public void Replay(IJourneyVisitor visitor)
        {
            IRideEvent previousEvent = null;
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

        private void DriveBetweenEvents(IJourneyVisitor visitor, IRideEvent previous, IRideEvent current)
        {
            if (IsDistanceBetween(previous, current))
            {
                visitor.Visit(CreateDrive(previous, current));
            }
        }

        private bool IsDistanceBetween(IRideEvent last, IRideEvent current)
        {
            return last.Distance < current.Distance;
        }

        private Drive CreateDrive(IRideEvent last, IRideEvent current)
        {
            return new Drive(new Distance(last.Distance, current.Distance));
        }
    }
}
