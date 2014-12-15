using System.Collections.Generic;
using System.Linq;
using Mors.Journeys.Application.QueryHandlers.Infrastructure.Views;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Data.Queries;
using Mors.Journeys.Data.Queries.Dtos;
using Period = Mors.Journeys.Application.QueryHandlers.Infrastructure.Period;

namespace Mors.Journeys.Application.QueryHandlers
{
    internal sealed class JourneyView
    {
        private readonly ValueSet<object, JourneyCreatedEvent> _journeys;
        private readonly ValueMultiSet<object, LiftAddedEvent> _lifts;

        public JourneyView()
        {
            _journeys = new ValueSet<object, JourneyCreatedEvent>(evt => evt.JourneyId);
            _lifts = new ValueMultiSet<object, LiftAddedEvent>(evt => evt.JourneyId);
        }

        public void Update(JourneyCreatedEvent @event)
        {
            _journeys.Add(@event);
        }

        public void Update(LiftAddedEvent @event)
        {
            _lifts.Add(@event);
        }

        public IEnumerable<Journey> Execute(GetJourneysInPeriodQuery query)
        {
            var period = new Period(query.Period.Start, query.Period.End);
            var journeysInPeriod = _journeys.Retrieve().Where(j => period.Contains(j.DateOfOccurrence));
            return journeysInPeriod.Select(ToDto).ToList();
        }

        private Journey ToDto(JourneyCreatedEvent journey)
        {
            var lifts = _lifts.GetOrDefault(journey.JourneyId);
            return new Journey(
                journey.JourneyId,
                journey.DateOfOccurrence,
                journey.RouteDistance,
                lifts.Select(ToDto).ToList());
        }

        private static Lift ToDto(LiftAddedEvent lift)
        {
            return new Lift(
                lift.PersonId,
                lift.LiftDistance);
        }
    }
}
