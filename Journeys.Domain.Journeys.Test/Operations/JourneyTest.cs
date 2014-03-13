using Journeys.Common;
using Journeys.Domain.Infrastructure.Exceptions;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.Test.Infrastructure;
using Journeys.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Journeys.Domain.Journeys.Test.Operations
{
    [TestClass]
    public class JourneyTest
    {
        private static readonly IId JourneyId = new Id(0);
        private static readonly IId PersonId = new Id(0);
        private EventBusMock _eventBus;

        [TestInitialize]
        public void TestInitialize()
        {
            _eventBus = new EventBusMock();
        }

        [TestMethod]
        public void ShouldPublishEventWhenAddingLift()
        {
            var journey = new Journey(JourneyId, new DateTime(), new Distance(10m, DistanceUnit.Kilometer), _eventBus);
            var liftDistance = new Distance(5m, DistanceUnit.Kilometer);

            var eventMatcher = _eventBus.Listen(() =>
            {
                journey.AddLift(PersonId, liftDistance);
            });

            eventMatcher.AssertReceivedOneEvent<LiftAddedEvent>(
                evt => evt.LiftDistance == liftDistance &&
                       evt.PersonId.Equals(PersonId));
        }

        [TestMethod]
        public void ShouldPublishEventWhenCreatingJourney()
        {
            var dateOfOccurence = new DateTime();
            var routeDistance = new Distance(10m, DistanceUnit.Kilometer);

            var eventMatcher = _eventBus.Listen(() =>
            {
                new Journey(JourneyId, dateOfOccurence, routeDistance, _eventBus);
            });

            eventMatcher.AssertReceivedOneEvent<JourneyCreatedEvent>(
                evt => evt.DateOfOccurrence == dateOfOccurence &&
                       evt.JourneyId == JourneyId &&
                       evt.RouteDistance == routeDistance);
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolationException))]
        public void ShouldReportInvariantViolationWhenAddingJourneyWithDistanceLargerThanRouteDistance()
        {
            var journey = new Journey(JourneyId, new DateTime(), new Distance(20m, DistanceUnit.Kilometer), _eventBus);

            journey.AddLift(PersonId, new Distance(40m, DistanceUnit.Kilometer));
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolationException))]
        public void ShouldReportInvariantViolationWhenGivenAJourneyWithLiftANewLiftIsAddedWithSamePerson()
        {
            var journey = new Journey(JourneyId, new DateTime(), new Distance(20m, DistanceUnit.Kilometer), _eventBus)
                .AddLift(PersonId, new Distance(10m, DistanceUnit.Kilometer));

            journey.AddLift(PersonId, new Distance(10m, DistanceUnit.Kilometer));
        }
    }
}
