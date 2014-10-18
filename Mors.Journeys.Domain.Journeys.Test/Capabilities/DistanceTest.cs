using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mors.Journeys.Domain.Journeys.Capabilities;

namespace Mors.Journeys.Domain.Journeys.Test.Capabilities
{
    [TestClass]
    public sealed class DistanceTest
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