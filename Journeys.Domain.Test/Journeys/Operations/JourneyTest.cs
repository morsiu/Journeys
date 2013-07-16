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
        public static readonly Id<Person> PersonId = new Id<Person>(new Guid());
        private EventBusMock _eventBus;
        private JourneyFactory _journeyFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            _eventBus = new EventBusMock();
            _journeyFactory = new JourneyFactory(_eventBus);
        }

        [TestMethod]
        public void ShouldPublishEventWhenAddingLift()
        {
            var journey = _journeyFactory.Create(new DateTime(), new Distance(10m, DistanceUnit.Kilometer));
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
        [ExpectedException(typeof(InvariantViolationException))]
        public void ShouldReportInvariantViolationWhenAddingJourneyWithDistanceLargerThanRouteDistance()
        {
            var journey = _journeyFactory.Create(new DateTime(), new Distance(20m, DistanceUnit.Kilometer));

            journey.AddLift(PersonId, new Distance(40m, DistanceUnit.Kilometer));
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolationException))]
        public void ShouldReportInvariantViolationWhenGivenAJourneyWithLiftANewLiftIsAddedWithSamePerson()
        {
            var journey = _journeyFactory.Create(new DateTime(), new Distance(20m, DistanceUnit.Kilometer))
                .AddLift(PersonId, new Distance(10m, DistanceUnit.Kilometer));

            journey.AddLift(PersonId, new Distance(10m, DistanceUnit.Kilometer));
        }
    }
}
