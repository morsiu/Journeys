using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Exceptions;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Events;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using Journeys.Domain.Test.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Journeys.Domain.Test.Journeys.Operations
{
    [TestClass]
    public class JourneyTest
    {
        public static readonly Id<Journey> JourneyId = new Id<Journey>(new Guid());
        public static readonly Id<Person> PersonId = new Id<Person>(new Guid());
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
                       evt.PersonId == PersonId);
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
                evt => evt.DateOfOccurence == dateOfOccurence &&
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
