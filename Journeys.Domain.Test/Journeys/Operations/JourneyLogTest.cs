using Journeys.Domain.Journeys.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Test.Journeys.Operations
{
    [TestClass]
    public class JourneyLogTest
    {
        [TestMethod]
        public void AddingJourneyToJourneyLogShouldIncreaseJourneyCountByOne()
        {
            var journeyLog = new JourneyLog();
            var journey = new Journey();

            journeyLog.AddJourney(journey);

            Assert.AreEqual(1, journeyLog.JourneyCount);
        }

        [TestMethod]
        public void EmptyJourneyLogShouldHaveZeroJourneyCount()
        {
            var journeyLog = new JourneyLog();

            Assert.AreEqual(0, journeyLog.JourneyCount);
        }
    }
}
