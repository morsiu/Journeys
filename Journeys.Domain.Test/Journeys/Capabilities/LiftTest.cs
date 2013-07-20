using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.People;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Journeys.Domain.Test.Journeys.Capabilities
{
    [TestClass]
    public class LiftTest
    {
        public static readonly Id PersonId = new Id(new Guid());
        public static readonly Id AnotherPersonId = new Id(Guid.NewGuid());

        [TestMethod]
        public void LiftWithPersonIdShouldBeEqualByPersonToThatId()
        {
            var lift = new Lift(PersonId, new Distance(1m, DistanceUnit.Kilometer));

            Assert.IsTrue(lift.EqualsByPerson(PersonId));
        }

        [TestMethod]
        public void LiftWithPersonIdShouldNotBeEqualByPersonToOtherId()
        {
            var lift = new Lift(PersonId, new Distance(1m, DistanceUnit.Kilometer));

            Assert.IsFalse(lift.EqualsByPerson(AnotherPersonId));
        }
    }
}
