using Journeys.Domain.Expenses.Capabilities;
using Journeys.Domain.Test.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Journeys.Domain.Expenses.Test.Capabilities
{
    [TestClass]
    public class LiftIdTest
    {
        [TestMethod]
        public void EqualsShouldReturnTrueForInstancesWithSameJourneyAndPersonIds()
        {
            var a = new LiftId(new Id(0), new Id(1));
            var b = new LiftId(new Id(0), new Id(1));

            var result = a.Equals(b);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void EqualsShouldReturnFalseForInstancesWithSameJourneyButDifferentPersonIds()
        {
            var a = new LiftId(new Id(0), new Id(1));
            var b = new LiftId(new Id(0), new Id(2));

            var result = a.Equals(b);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void EqualsShouldReturnFalseForInstancesWithSamePersonButDifferentJourneyIds()
        {
            var a = new LiftId(new Id(0), new Id(2));
            var b = new LiftId(new Id(1), new Id(2));

            var result = a.Equals(b);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void EqualsShouldReturnFalseForInstancesWhereSecondIsNotLiftId()
        {
            var a = new LiftId(new Id(0), new Id(1));
            var b = new Id();

            var result = a.Equals(b);

            Assert.AreEqual(false, result);
        }
    }
}
