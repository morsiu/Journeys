using System;
using Mors.Journeys.Application;
using Mors.Journeys.Data.Commands;
using Mors.Journeys.Data.Queries;
using Mors.Journeys.Domain.Journeys.Capabilities;
using Mors.Journeys.Domain.Journeys.Operations;
using Mors.Journeys.Domain.People;

namespace Mors.Journeys.Application.CommandHandlers
{
    internal sealed class AddJourneyWithLiftsCommandHandler
    {
        private readonly Action<object> _eventPublisher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IRepositories _repositories;
        private readonly Func<object> _idFactory;

        public AddJourneyWithLiftsCommandHandler(
            Action<object> eventPublisher,
            IRepositories repositories,
            Func<object> idFactory,
            IQueryDispatcher queryDispatcher)
        {
            _idFactory = idFactory;
            _queryDispatcher = queryDispatcher;
            _repositories = repositories;
            _eventPublisher = eventPublisher;
        }

        public void Execute(AddJourneyWithLiftsCommand command)
        {
            var routeDistance = new Distance(command.RouteDistance, DistanceUnit.Kilometer);
            var journey = new Journey(command.JourneyId, command.DateOfOccurrence, routeDistance, _eventPublisher);
            foreach (var lift in command.Lifts)
            {
                var liftDistance = new Distance(lift.LiftDistance, DistanceUnit.Kilometer);
                var personId = GetOrAddPersonWithName(lift.PersonName);
                journey = journey.AddLift(personId, liftDistance);
            }
            _repositories.Store(journey);
        }

        private object GetOrAddPersonWithName(string personName)
        {
            var personId = _queryDispatcher.Dispatch(new GetPersonIdByNameQuery(personName));
            if (personId == null)
            {
                personId = _idFactory();
                var person = new Person(personId, personName, _eventPublisher);
                _repositories.Store(person);
            }
            return personId;
        }
    }
}
