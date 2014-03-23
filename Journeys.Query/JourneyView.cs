using Journeys.Common;
using Journeys.Events;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using Journeys.Query.Infrastructure.Views;
using System.Collections.Generic;
using System.Linq;
using Period = Journeys.Query.Infrastructure.Period;

namespace Journeys.Query
{
    internal class JourneyView
    {
        private readonly ValueSet<IId, JourneyCreatedEvent> _journeys;
        private readonly ValueMultiSet<IId, LiftAddedEvent> _lifts;

        public JourneyView()
        {
            _journeys = new ValueSet<IId, JourneyCreatedEvent>(evt => evt.JourneyId);
            _lifts = new ValueMultiSet<IId, LiftAddedEvent>(evt => evt.JourneyId);
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
            return new Journeys.Queries.Dtos.Journey(
                journey.JourneyId,
                journey.DateOfOccurrence,
                journey.RouteDistance,
                lifts.Select(ToDto).ToList());
        }

        private Lift ToDto(LiftAddedEvent lift)
        {
            return new Journeys.Queries.Dtos.Lift(
                lift.PersonId,
                lift.LiftDistance);
        }
    }
}
