using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Events;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.Test.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Test.Journeys.Operations
{
    [TestClass]
    public class JourneyFactoryTest
    {
        [TestMethod]
        public void ShouldPublishEventWhenCreatingJourney()
        {
            var eventBusMock = new EventBusMock();
            var factory = new JourneyFactory(eventBusMock);
            var dateOfOccurence = new DateTime(2, 2, 2);
            var routeDistance = new Distance(20m, DistanceUnit.Kilometer);

            var eventMatcher = eventBusMock.Listen(() =>
            {
                factory.Create(dateOfOccurence, routeDistance);
            });

            eventMatcher.AssertReceivedOneEvent<JourneyCreatedEvent>(
                evt => evt.DateOfOccurence == dateOfOccurence &&
                       evt.RouteDistance == routeDistance);
        }
    }
}
