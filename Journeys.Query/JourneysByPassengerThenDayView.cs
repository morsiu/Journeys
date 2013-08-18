using System;
using System.Collections.Generic;
using System.Linq;
using Journeys.Events;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using Journeys.Query.Infrastructure.Views;
using PassengerId = System.Guid;
using Date = System.DateTime;

namespace Journeys.Query
{
    using JourneyId = Guid;
    using Journey = JourneyCreatedEvent;
    using Lift = LiftAddedEvent;
    using JourneysPerDayByPassengerLookup = Infrastructure.Views.Lookup<PassengerId, Set<Date, JourneysOnDay>>;

    internal class JourneysByPassengerThenDayView
    {
        private readonly Set<JourneyId, Journey> _journeys = new Set<JourneyId, Journey>(GetKey);
        private readonly JourneysPerDayByPassengerLookup _journeysPerDayByPassenger = new JourneysPerDayByPassengerLookup();

        public IEnumerable<JourneysOnDay> Execute(GetJourneysByDayForPassengerInPeriodQuery query)
        {
            var journeysByDayForPassenger = _journeysPerDayByPassenger.Get(query.PassengerId, () => new Set<DateTime, JourneysOnDay>(GetKey));
            var journeysPerDay = journeysByDayForPassenger.Retrieve();
            return journeysPerDay
                .Where(i => i.Day <= query.PeriodEnd.Date && i.Day >= query.PeriodStart.Date)
                .OrderBy(i => i.Day);
        }

        public void Update(JourneyCreatedEvent @event)
        {
            _journeys.Add(@event);
        }

        public void Update(LiftAddedEvent @event)
        {
            var journey = _journeys.Get(@event.JourneyId);
            var journeysByDay = _journeysPerDayByPassenger.GetOrAdd(@event.PersonId, () => new Set<DateTime, JourneysOnDay>(GetKey));
            var date = journey.DateOfOccurrence.Date;
            journeysByDay.UpdateOrAdd(
                date,
                () => new JourneysOnDay(date, 0, 0m, 0m),
                oldJourneyByDay => AddLiftToJourneyOnDay(oldJourneyByDay, @event, journey));
        }

        private static JourneysOnDay AddLiftToJourneyOnDay(JourneysOnDay journeysOnDay, Lift lift, Journey journey)
        {
            return new JourneysOnDay(
                journeysOnDay.Day,
                journeysOnDay.JourneyCount + 1,
                journeysOnDay.TotalRouteDistance + journey.RouteDistance,
                journeysOnDay.TotalLiftDistance + lift.LiftDistance);
        }

        private static Guid GetKey(Journey @event)
        {
            return @event.JourneyId;
        }

        private static DateTime GetKey(JourneysOnDay journeysOnDay)
        {
            return journeysOnDay.Day;
        }
    }
}