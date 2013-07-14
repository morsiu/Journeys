using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.People;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Test.Journeys.Capabilities
{
    [TestClass]
    public class LiftTest
    {
        [TestMethod]
        public void LiftWithPersonIdShouldBeEqualByPersonToThatId()
        {
            var lift = new Lift(new Id<Person>(1), new Distance(1m, DistanceUnit.Kilometer));

            Assert.IsTrue(lift.EqualsByPerson(new Id<Person>(1)));
        }

        [TestMethod]
        public void LiftWithPersonIdShouldNotBeEqualByPersonToOtherId()
        {
            var lift = new Lift(new Id<Person>(1), new Distance(1m, DistanceUnit.Kilometer));

            Assert.IsFalse(lift.EqualsByPerson(new Id<Person>(2)));
        }
    }
}
