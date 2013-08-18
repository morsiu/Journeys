using System;
using System.Collections.Generic;
using System.Linq;
using Journeys.Events;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using Journeys.Query.Infrastructure.Views;

namespace Journeys.Query
{
    using EmptyJourney = JourneyCreatedEvent;
    using JourneyWithLiftKey = Tuple<Guid, Guid>;

    internal class JourneysWithLiftsView
    {
        private readonly Set<Guid, EmptyJourney> _emptyJourneys = new Set<Guid, EmptyJourney>(GetKey);
        private readonly Set<JourneyWithLiftKey, JourneyWithLift> _journeysWithLifts = new Set<JourneyWithLiftKey, JourneyWithLift>(GetKey);
        private readonly IQueryDispatcher _queryDispather;

        public JourneysWithLiftsView(IQueryDispatcher queryDispatcher)
        {
            _queryDispather = queryDispatcher;
        }

        public IEnumerable<JourneyWithLift> Execute(GetAllJourneysWithLiftsQuery query)
        {
            return GetSortedJourneysWithLifts()
                .ToList();
        }

        public IEnumerable<JourneyWithLift> Execute(GetJourneysWithLiftsByJourneyIdQuery query)
        {
            return GetSortedJourneysWithLifts()
                .Where(e => e.JourneyId == query.JourneyId)
                .ToList();
        }

        public void Update(JourneyCreatedEvent @event)
        {
            _emptyJourneys.Add(@event);
        }

        public void Update(LiftAddedEvent @event)
        {
            var emptyJourney = _emptyJourneys.Get(@event.JourneyId);
            var journeyWithLift = Create(emptyJourney, @event);
            _journeysWithLifts.Add(journeyWithLift);
        }

        private IEnumerable<JourneyWithLift> GetSortedJourneysWithLifts()
        {
            return _journeysWithLifts.Retrieve()
                .OrderBy(e => e.DateOfOccurrence)
                .ThenBy(e => e.JourneyId)
                .ThenBy(e => e.PassengerName)
                .ThenBy(e => e.PassengerId);
        }

        private JourneyWithLift Create(EmptyJourney emptyJourney, LiftAddedEvent liftAddedEvent)
        {
            var passengerName = GetPassengerName(liftAddedEvent.PersonId);
            return new JourneyWithLift(
                emptyJourney.JourneyId,
                liftAddedEvent.PersonId,
                emptyJourney.DateOfOccurrence,
                emptyJourney.RouteDistance,
                passengerName,
                liftAddedEvent.LiftDistance);
        }

        private static JourneyWithLiftKey GetKey(JourneyWithLift value)
        {
            return Tuple.Create(value.JourneyId, value.PassengerId);
        }

        private static Guid GetKey(EmptyJourney @event)
        {
            return @event.JourneyId;
        }

        private string GetPassengerName(Guid personId)
        {
            return _queryDispather.Dispatch(new GetPersonNameQuery(personId));
        }
    }
}
