using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Exceptions;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Journeys.Domain.Test.Journeys.Operations
{
    [TestClass]
    public class JourneyTest
    {
        public static readonly Id<Person> PersonId = new Id<Person>(new Guid());

        [TestMethod]
        public void ShouldBeAbleToAddLift()
        {
            var journeyFactory = new JourneyFactory();
            var journey = journeyFactory.Create(new DateTime(), new Distance(10m, DistanceUnit.Kilometer));

            journey.AddLift(PersonId, new Distance(5m, DistanceUnit.Kilometer));
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolatedException))]
        public void ShouldReportInvariantViolationWhenAddingJourneyWithDistanceLargerThanRouteDistance()
        {
            var journeyFactory = new JourneyFactory();
            var journey = journeyFactory.Create(new DateTime(), new Distance(20m, DistanceUnit.Kilometer));

            journey.AddLift(PersonId, new Distance(40m, DistanceUnit.Kilometer));
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolatedException))]
        public void ShouldReportInvariantViolationWhenGivenAJourneyWithLiftANewLiftIsAddedWithSamePerson()
        {
            var journeyFactory = new JourneyFactory();
            var journey = journeyFactory.Create(new DateTime(), new Distance(20m, DistanceUnit.Kilometer));
            journey.AddLift(PersonId, new Distance(10m, DistanceUnit.Kilometer));

            journey.AddLift(PersonId, new Distance(10m, DistanceUnit.Kilometer));
        }
    }
}
