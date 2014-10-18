using System;
using System.Collections.Generic;
using System.Linq;
using Mors.Journeys.Application.QueryHandlers.Infrastructure.Views;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Data.Queries;
using Mors.Journeys.Data.Queries.Dtos.JourneysByPassengerThenMonthThenDay;

namespace Mors.Journeys.Application.QueryHandlers
{
    internal sealed class JourneysByPassengerThenMonthThenDayView
    {
        private readonly ValueLookup<Key, Value> _facts = new ValueLookup<Key, Value>();
        private readonly ValueSet<object, JourneyCreatedEvent> _journeys = new ValueSet<object, JourneyCreatedEvent>(journey => journey.JourneyId);
        private readonly ValueLookup<DateTime, HashSet<JourneyCreatedEvent>> _journeysByDate = new ValueLookup<DateTime, HashSet<JourneyCreatedEvent>>();

        public IEnumerable<Fact> Execute(GetJourneysByPassengerThenMonthThenDayQuery query)
        {
            return _facts.Retrieve().Select(factEntry => new Fact(factEntry.Key, factEntry.Value)).ToList();
        }

        public void Update(JourneyCreatedEvent @event)
        {
            _journeys.Add(@event);
            var journeysOnDateOfOccurence = _journeysByDate.GetOrAdd(@event.DateOfOccurrence, () => new HashSet<JourneyCreatedEvent>());
            journeysOnDateOfOccurence.Add(@event);
            var dateOfOccurrence = @event.DateOfOccurrence;
            foreach (var factEntry in _facts.Retrieve().Where(f => f.Key.Day.DayOfMonth == dateOfOccurrence.Day && f.Key.Month.MonthOfYear == dateOfOccurrence.Month && f.Key.Month.Year == dateOfOccurrence.Year).ToList())
            {
                _facts.Set(factEntry.Key, UpdateValue(factEntry.Value, @event));
            }
        }

        public void Update(LiftAddedEvent @event)
        {
            var journey = _journeys.Get(@event.JourneyId);
            var dateOfOccurrence = journey.DateOfOccurrence;
            var key = new Key(new Passenger(@event.PersonId), new Month(dateOfOccurrence.Year, dateOfOccurrence.Month), new Day(dateOfOccurrence.Day));
            var value = _facts.GetOrAdd(key, () => CreateValue(dateOfOccurrence));
            _facts.Set(key, UpdateValue(value, @event));
        }

        private Value UpdateValue(Value value, JourneyCreatedEvent @event)
        {
            return new Value(
                value.JourneyCount + 1,
                value.JourneyDistance + @event.RouteDistance,
                value.LiftCount,
                value.LiftDistance);
        }

        private Value UpdateValue(Value value, LiftAddedEvent @event)
        {
            return new Value(
                value.JourneyCount,
                value.JourneyDistance,
                value.LiftCount + 1,
                value.LiftDistance + @event.LiftDistance);
        }

        private Value CreateValue(DateTime dateOfOccurrence)
        {
            var journeys = _journeysByDate.Get(dateOfOccurrence).Value;
            return new Value(
                journeys.Count,
                journeys.Sum(j => j.RouteDistance),
                0,
                0m);
        }
    }
}
