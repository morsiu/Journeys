using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Domain.Infrastructure.Exceptions;
using Mors.Journeys.Domain.Test;

namespace Mors.Journeys.Domain.People.Test
{
    [TestClass]
    public sealed class PersonTest
    {
        private static readonly string PersonName = "PersonName";
        private static readonly object PersonId = new Id(0);
        private EventBus _eventBus;

        [TestInitialize]
        public void TestInitialize()
        {
            _eventBus = new EventBus();
        }

        [TestMethod]
        public void ShouldPublishEventWhenCreatingPerson()
        {
            var eventMatcher = _eventBus.Listen(() =>
            {
                new Person(PersonId, PersonName, _eventBus.Publish);
            });

            eventMatcher.AssertReceivedOneEvent<PersonCreatedEvent>(
                evt => evt.PersonId.Equals(PersonId) &&
                       evt.PersonName == PersonName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolationException))]
        public void ShouldReportInvariantViolationWhenCreatingPersonWithEmptyName()
        {
            new Person(PersonId, string.Empty, _eventBus.Publish);
        }

        [TestMethod]
        [ExpectedException(typeof(InvariantViolationException))]
        public void ShouldReportInvariantViolationWhenCreatingPersonWithNullName()
        {
            new Person(PersonId, null, _eventBus.Publish);
        }
    }
}
