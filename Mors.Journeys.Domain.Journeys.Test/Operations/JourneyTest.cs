using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Domain.Infrastructure.Exceptions;
using Mors.Journeys.Domain.Journeys.Capabilities;
using Mors.Journeys.Domain.Journeys.Operations;
using Mors.Journeys.Domain.Test;

namespace Mors.Journeys.Domain.Journeys.Test.Operations
{
    [TestClass]
    public sealed class JourneyTest
    {
        private static readonly object JourneyId = new Id(0);
        private static readonly object PersonId = new Id(0);
        private EventBus _eventBus;

        [TestInitialize]
        public void TestInitialize()
        {
            _eventBus = new EventBus();
        }

        [TestMethod]
        public void ShouldPublishEventWhenAddingLift()
        {
            var journey = new Journey(JourneyId, new DateTime(), new Distance(10m, DistanceUnit.Kilometer), _eventBus.Publish);
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
                new Journey(JourneyId, dateOfOccurence, routeDistance, _eventBus.Publish);
            });

            eventMatcher.AssertReceivedOneEvent<JourneyCreatedEvent>(
                evt => evt.DateOfOccurrence == dateOfOccurence &&
                       evt.JourneyId == JourneyId &&
                       evt.RouteDistance == routeDistance);
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolationException))]
        public void ShouldReportInvariantViolationWhenAddingLiftWithDistanceLargerThanRouteDistance()
        {
            var journey = new Journey(JourneyId, new DateTime(), new Distance(20m, DistanceUnit.Kilometer), _eventBus.Publish);

            journey.AddLift(PersonId, new Distance(40m, DistanceUnit.Kilometer));
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolationException))]
        public void ShouldReportInvariantViolationWhenGivenAJourneyWithLiftANewLiftIsAddedWithSamePerson()
        {
            var journey = new Journey(JourneyId, new DateTime(), new Distance(20m, DistanceUnit.Kilometer), _eventBus.Publish)
                .AddLift(PersonId, new Distance(10m, DistanceUnit.Kilometer));

            journey.AddLift(PersonId, new Distance(10m, DistanceUnit.Kilometer));
        }
    }
}
