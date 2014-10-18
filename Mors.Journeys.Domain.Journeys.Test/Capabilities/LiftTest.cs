using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mors.Journeys.Domain.Journeys.Capabilities;
using Mors.Journeys.Domain.Test;

namespace Mors.Journeys.Domain.Journeys.Test.Capabilities
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
