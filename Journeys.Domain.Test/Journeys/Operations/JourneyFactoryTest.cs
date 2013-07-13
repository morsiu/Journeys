using Journeys.Domain.Exceptions;
using Journeys.Domain.Identities;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using Journeys.Domain.Routes.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Journeys.Domain.Test.Journeys.Operations
{
    [TestClass]
    public class JourneyFactoryTest
    {
        [TestMethod]
        public void ShouldBeAbleToSetDateOfJourney()
        {
            var factory = new JourneyFactory();

            factory.SetDateOfOccurence(new DateTime());
        }

        [TestMethod]
        public void ShouldBeAbleToAddLift()
        {
            var factory = new JourneyFactory();

            factory.AddLift(new Id<Person>(1), new Distance(1m, DistanceUnit.Kilometer));
        }

        [TestMethod]
        public void ShouldBeAbleToSetRoute()
        {
            var factory = new JourneyFactory();

            factory.SetRoute(new Id<Route>(1));
        }

        [TestMethod]
        public void ShouldBeAbleToBuildJourney()
        {
            var factory = new JourneyFactory();

            factory.SetDateOfOccurence(new DateTime());
            factory.SetRoute(new Id<Route>(1));
            factory.AddLift(new Id<Person>(1), new Distance(1m, DistanceUnit.Kilometer));

            factory.BuildJourney();
        }

        [TestMethod]
        [ExpectedException(typeof(EntityBuildException))]
        public void ShouldNotBuildWithoutDateOfOccurence()
        {
            var factory = new JourneyFactory();
            factory.SetRoute(new Id<Route>(1));

            factory.BuildJourney();
        }

        [TestMethod]
        [ExpectedException(typeof(EntityBuildException))]
        public void ShouldNotBuildWithoutRouteId()
        {
            var factory = new JourneyFactory();
            factory.SetDateOfOccurence(new DateTime());

            factory.BuildJourney();
        }
    }
}
