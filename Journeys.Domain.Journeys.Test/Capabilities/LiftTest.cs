using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Test.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Journeys.Domain.Journeys.Test.Capabilities
{
    [TestClass]
    public sealed class LiftTest
    {
        private static readonly object PersonId = new Id(0);
        private static readonly object AnotherPersonId = new Id(1);

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
