using System;
using Journeys.Common;
using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.People;
using Journeys.Domain.Test.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Journeys.Domain.Test.Journeys.Capabilities
{
    [TestClass]
    public class LiftTest
    {
        private static readonly IId PersonId = new Id(0);
        private static readonly IId AnotherPersonId = new Id(1);

        [TestMethod]
        public void LiftWithPersonIdShouldBeForPersonWithThatId()
        {
            var lift = new Lift(PersonId);

            Assert.IsTrue(lift.IsForPerson(PersonId));
        }

        [TestMethod]
        public void LiftWithPersonIdShouldNotBeForPersonWithDifferentId()
        {
            var lift = new Lift(PersonId);

            Assert.IsFalse(lift.IsForPerson(AnotherPersonId));
        }
    }
}
