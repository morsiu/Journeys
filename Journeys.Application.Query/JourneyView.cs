using System.Collections.Generic;
using System.Linq;
using Journeys.Application.Query.Infrastructure.Views;
using Journeys.Data.Events;
using Journeys.Data.Queries;
using Journeys.Data.Queries.Dtos;
using Period = Journeys.Application.Query.Infrastructure.Period;

namespace Journeys.Application.Query
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
            return new Journeys.Data.Queries.Dtos.Journey(
                journey.JourneyId,
                journey.DateOfOccurrence,
                journey.RouteDistance,
                lifts.Select(ToDto).ToList());
        }

        private Lift ToDto(LiftAddedEvent lift)
        {
            return new Journeys.Data.Queries.Dtos.Lift(
                lift.PersonId,
                lift.LiftDistance);
        }
    }
}
