using System;
using System.Collections.Generic;
using System.Linq;
using Journeys.Events;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using Journeys.Query.Infrastructure.Views;

namespace Journeys.Query
{
    using PassengerId = Guid;
    using JourneyId = Guid;
    using Journey = JourneyCreatedEvent;
    using Lift = LiftAddedEvent;
    using Date = DateTime;

    internal class JourneysByPassengerThenDayView
    {
        private readonly Set<JourneyId, Journey> _journeys = new Set<JourneyId, Journey>(GetKey);
        private readonly Infrastructure.Views.Lookup<PassengerId, Set<Date, JourneysByDay>> _journeysByPassengerThenDate = new Infrastructure.Views.Lookup<PassengerId, Set<Date, JourneysByDay>>();

        public void Handle(JourneyCreatedEvent @event)
        {
            _journeys.Add(@event);
        }

        public void Handle(LiftAddedEvent @event)
        {
            var journey = _journeys.Get(@event.JourneyId);
            var journeysByDay = _journeysByPassengerThenDate.GetOrAdd(@event.PersonId, () => new Set<DateTime, JourneysByDay>(GetKey));
            var date = journey.DateOfOccurrence.Date;
            journeysByDay.UpdateOrAdd(
                date,
                () => new JourneysByDay(date, 0, 0m, 0m),
                oldJourneyByDay => AddJourney(oldJourneyByDay, journey, @event));
        }

        public IEnumerable<JourneysByDay> Execute(GetJourneysByDayForPassengerInPeriodQuery query)
        {
            var journeysByDayForPassenger = _journeysByPassengerThenDate.Get(query.PassengerId, () => new Set<DateTime, JourneysByDay>(GetKey));
            var journeysByDays = journeysByDayForPassenger.Retrieve();
            return Enumerable.Where(
                journeysByDays,
                i => i.Date <= query.PeriodEnd.Date && i.Date >= query.PeriodStart.Date);
        }

        private static Guid GetKey(Journey @event)
        {
            return @event.JourneyId;
        }

        private static JourneysByDay AddJourney(JourneysByDay journeysByDay, Journey journey, Lift lift)
        {
            return new JourneysByDay(
                journeysByDay.Date,
                journeysByDay.JourneyCount + 1,
                journeysByDay.TotalJourneysDistance + journey.RouteDistance,
                journeysByDay.TotalLiftDistance + lift.LiftDistance);
        }

        private static DateTime GetKey(JourneysByDay journeysByDay)
        {
            return journeysByDay.Date;
        }
    }
}