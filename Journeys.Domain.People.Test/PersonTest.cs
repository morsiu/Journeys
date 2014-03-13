using Journeys.Common;
using Journeys.Domain.Infrastructure.Exceptions;
using Journeys.Domain.Test.Infrastructure;
using Journeys.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Journeys.Domain.People.Test
{
    [TestClass]
    public class PersonTest
    {
        private static readonly string PersonName = "PersonName";
        private static readonly IId PersonId = new Id(0);
        private EventBusMock _eventBus;

        [TestInitialize]
        public void TestInitialize()
        {
            _eventBus = new EventBusMock();
        }

        [TestMethod]
        public void ShouldPublishEventWhenCreatingPerson()
        {
            var eventMatcher = _eventBus.Listen(() =>
            {
                new Person(PersonId, PersonName, _eventBus.Object);
            });

            eventMatcher.AssertReceivedOneEvent<PersonCreatedEvent>(
                evt => evt.PersonId.Equals(PersonId) &&
                       evt.PersonName == PersonName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolationException))]
        public void ShouldReportInvariantViolationWhenCreatingPersonWithEmptyName()
        {
            new Person(PersonId, string.Empty, _eventBus.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolationException))]
        public void ShouldReportInvariantViolationWhenCreatingPersonWithNullName()
        {
            new Person(PersonId, null, _eventBus.Object);
        }
    }
}
