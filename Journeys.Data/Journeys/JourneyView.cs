using Journeys.Events;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Data.Journeys
{
    internal class JourneyView
    {
        private HashSet<JourneyWithLift> _elements = new HashSet<JourneyWithLift>(new Comparer());

        public IEnumerable<JourneyWithLift> GetAllJourneysWithLifts()
        {
            return _elements
                .Where(e => e.PassengerId.HasValue)
                .OrderBy(e => e.DateOfOccurence)
                .ThenBy(e => e.Id)
                .ThenBy(e => e.PassengerName)
                .ThenBy(e => e.PassengerId)
                .ToList();
        }

        public void Update(JourneyCreatedEvent @event)
        {
            var newElement =
                new JourneyWithLift
                {
                    Id = @event.JourneyId,
                    Distance = @event.RouteDistance,
                    DateOfOccurence = @event.DateOfOccurence,
                };
            _elements.Add(newElement);
        }

        public void Update(LiftAddedEvent @event)
        {
            var element = _elements.SingleOrDefault(e => e.Id == @event.JourneyId && e.PassengerId == @event.PersonId);
            if (element == null)
            {
                var emptyElement = _elements.Single(e => e.Id == @event.JourneyId && !e.PassengerId.HasValue);
                element =
                    new JourneyWithLift
                    {
                        Id = @event.JourneyId,
                        DateOfOccurence = emptyElement.DateOfOccurence,
                        Distance = emptyElement.Distance,
                        PassengerId = @event.PersonId,
                        PassengerLiftDistance = @event.LiftDistance
                    };
            }
            element.PassengerLiftDistance = @event.LiftDistance;
            element.PassengerId = @event.PersonId;
            _elements.Add(element);
        }

        private class Comparer : IEqualityComparer<JourneyWithLift>
        {
            public bool Equals(JourneyWithLift x, JourneyWithLift y)
            {
                return x.Id == y.Id && x.PassengerId == y.PassengerId;
            }

            public int GetHashCode(JourneyWithLift obj)
            {
                return obj.Id.GetHashCode() * 37 + obj.PassengerId.GetHashCode();
            }
        }
    }
}
