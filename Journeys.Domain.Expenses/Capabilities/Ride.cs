using Journeys.Common;
using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Expenses.Capabilities.RideEvents;
using Journeys.Domain.Expenses.Policies;
using System.Collections.Generic;

namespace Journeys.Domain.Expenses.Operations
{
    internal class Ride
    {
        private readonly List<IRideEvent> _events = new List<IRideEvent>();
        private Point _distance;

        public Ride(Point rideDistance)
        {
            _distance = rideDistance;
        }

        public void IncludeLift(IId passengerId, Distance distance)
        {
            _events.Add(new PassengerPickup(passengerId, distance.From));
            _events.Add(new PassengerExit(passengerId, distance.To));
            _events.Sort((a, b) => a.Distance.CompareTo(b.Distance));
        }

        public void Replay(IRideVisitor visitor)
        {
            var start = CreateStart();
            start.Visit(visitor);
            IRideEvent lastEvent = start;
            using (var events = _events.GetEnumerator())
            {
                while (events.MoveNext())
                {
                    var currentEvent = events.Current;
                    DriveBetweenEvents(visitor, lastEvent, currentEvent);
                    currentEvent.Visit(visitor);
                    lastEvent = currentEvent;
                }
            }
            var finish = CreateFinish();
            DriveBetweenEvents(visitor, lastEvent, finish);
            finish.Visit(visitor);
        }

        private IRideEvent CreateFinish()
        {
            return new RideFinish(_distance);
        }

        private IRideEvent CreateStart()
        {
            return new RideStart();
        }

        private void DriveBetweenEvents(IRideVisitor visitor, IRideEvent last, IRideEvent current)
        {
            if (IsDistanceBetween(last, current))
            {
                visitor.Visit(CreateDrive(last, current));
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
