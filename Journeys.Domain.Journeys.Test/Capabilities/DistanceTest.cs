using Journeys.Domain.Journeys.Capabilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Journeys.Domain.Journeys.Test.Capabilities
{
    [TestClass]
    public class DistanceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotConstructWithNegativeAmount()
        {
            var negativeAmount = -4m;

            new Distance(negativeAmount, DistanceUnit.Kilometer);
        }

        [TestMethod]
        public void DistanceWithEqualAmountsInKilometersShouldBeEqual()
        {
            var distance = new Distance(2m, DistanceUnit.Kilometer);

            Assert.AreEqual(true, distance.Equals(distance));
        }

        [TestMethod]
        public void DistanceWithSmallerAmountInKilometersShouldBeSmaller()
        {
            var smallerDistance = new Distance(3m, DistanceUnit.Kilometer);
            var largerDistance = new Distance(5m, DistanceUnit.Kilometer);

            Assert.IsTrue(smallerDistance < largerDistance);
        }

        [TestMethod]
        public void DistanceWithLargerAmountInKilometersShouldBeLarger()
        {
            var largerDistance = new Distance(5m, DistanceUnit.Kilometer);
            var smallerDistance = new Distance(3m, DistanceUnit.Kilometer);

            Assert.IsTrue(largerDistance > smallerDistance);
        }
    }
}