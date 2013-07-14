using Journeys.Domain.Journeys.Capabilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Test.Journeys.Capabilities
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

            Assert.AreEqual(0, distance.CompareTo(distance));
        }

        [TestMethod]
        public void DistanceWithSmallerAmountInKilometersShouldBeSmaller()
        {
            var smallerDistance = new Distance(3m, DistanceUnit.Kilometer);
            var largerDistance = new Distance(5m, DistanceUnit.Kilometer);

            Assert.IsTrue(smallerDistance.CompareTo(largerDistance) < 0);
        }

        [TestMethod]
        public void DistanceWithLargerAmountInKilometersShouldBeLarger()
        {
            var largerDistance = new Distance(5m, DistanceUnit.Kilometer);
            var smallerDistance = new Distance(3m, DistanceUnit.Kilometer);

            Assert.IsTrue(largerDistance.CompareTo(smallerDistance) > 0);
        }
    }
}