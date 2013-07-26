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
        private readonly HashSet<JourneyWithLift> _elements = new HashSet<JourneyWithLift>(new Comparer());
        private readonly IQueryDispatcher _queryDispather;

        public JourneyView(IQueryDispatcher queryDispatcher)
        {
            _queryDispather = queryDispatcher;
        }

        public IEnumerable<JourneyWithLift> GetAllJourneysWithLifts()
        {
            return _elements
                .Where(e => e.PassengerId.HasValue)
                .OrderBy(e => e.DateOfOccurrence)
                .ThenBy(e => e.JourneyId)
                .ThenBy(e => e.PassengerName)
                .ThenBy(e => e.PassengerId)
                .ToList();
        }

        public void Update(JourneyCreatedEvent @event)
        {
            var newElement =
                new JourneyWithLift
                {
                    JourneyId = @event.JourneyId,
                    Distance = @event.RouteDistance,
                    DateOfOccurrence = @event.DateOfOccurrence,
                };
            _elements.Add(newElement);
        }

        public void Update(LiftAddedEvent @event)
        {
            var element = _elements.SingleOrDefault(e => e.JourneyId == @event.JourneyId && e.PassengerId == @event.PersonId);
            if (element == null)
            {
                var emptyElement = _elements.Single(e => e.JourneyId == @event.JourneyId && !e.PassengerId.HasValue);
                element =
                    new JourneyWithLift
                    {
                        JourneyId = @event.JourneyId,
                        DateOfOccurrence = emptyElement.DateOfOccurrence,
                        Distance = emptyElement.Distance,
                        PassengerId = @event.PersonId,
                        PassengerName = GetPassengerName(@event.PersonId),
                        PassengerLiftDistance = @event.LiftDistance
                    };
            }
            element.PassengerLiftDistance = @event.LiftDistance;
            element.PassengerId = @event.PersonId;
            _elements.Add(element);
        }

        private string GetPassengerName(Guid personId)
        {
            return _queryDispather.Dispatch(new GetPersonNameQuery(personId));
        }
        private class Comparer : IEqualityComparer<JourneyWithLift>
        {
            public bool Equals(JourneyWithLift x, JourneyWithLift y)
            {
                return x.JourneyId == y.JourneyId && x.PassengerId == y.PassengerId;
            }

            public int GetHashCode(JourneyWithLift obj)
            {
                return obj.JourneyId.GetHashCode() * 37 + obj.PassengerId.GetHashCode();
            }
        }
    }
}
