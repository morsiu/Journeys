using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Exceptions;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Test.Journeys.Operations
{
    [TestClass]
    public class JourneyTest
    {
        [TestMethod]
        public void ShouldBeAbleToAddLift()
        {
            var journey = new Journey(new DateTime(), new Distance(10m, DistanceUnit.Kilometer));

            journey.AddLift(new Id<Person>(1), new Distance(5m, DistanceUnit.Kilometer));
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolatedException))]
        public void ShouldReportInvariantViolationWhenAddingJourneyWithDistanceLargerThanRouteDistance()
        {
            var journey = new Journey(new DateTime(), new Distance(20m, DistanceUnit.Kilometer));

            journey.AddLift(new Id<Person>(1), new Distance(40m, DistanceUnit.Kilometer));
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolatedException))]
        public void ShouldReportInvariantViolationWhenGivenAJourneyWithLiftANewLiftIsAddedWithSamePerson()
        {
            var journey = new Journey(new DateTime(), new Distance(20m, DistanceUnit.Kilometer));
            journey.AddLift(new Id<Person>(1), new Distance(10m, DistanceUnit.Kilometer));

            journey.AddLift(new Id<Person>(1), new Distance(10m, DistanceUnit.Kilometer));
        }
    }
}
