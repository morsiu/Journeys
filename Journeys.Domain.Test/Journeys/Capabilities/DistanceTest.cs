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
    }
}
