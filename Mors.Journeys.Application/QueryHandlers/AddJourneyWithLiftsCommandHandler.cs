using Mors.AppPlatform.Common.Services;
using Mors.AppPlatform.Common.Transactions;
using Mors.Journeys.Data.Commands;
using Mors.Journeys.Data.Queries;
using Mors.Journeys.Domain.Journeys.Capabilities;
using Mors.Journeys.Domain.Journeys.Operations;
using Mors.Journeys.Domain.People;

namespace Mors.Journeys.Application.CommandHandlers
{
    internal sealed class AddJourneyWithLiftsCommandHandler
    {
        private readonly Transaction _transaction;
        private readonly IEventBus _eventBus;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IRepositories _repositories;
        private readonly IIdFactory _idFactory;

        public AddJourneyWithLiftsCommandHandler(
            IEventBus eventBus,
            IRepositories repositories,
            IIdFactory idFactory,
            IQueryDispatcher queryDispatcher)
        {
            _transaction = new Transaction();
            _idFactory = idFactory;
            _repositories = _transaction.Register(repositories);
            _eventBus = _transaction.Register(eventBus);
            _queryDispatcher = queryDispatcher;
        }

        public void ExecuteTransacted(AddJourneyWithLiftsCommand command)
        {
            _transaction.Run(() => Execute(command));
        }

        private void Execute(AddJourneyWithLiftsCommand command)
        {
            var routeDistance = new Distance(command.RouteDistance, DistanceUnit.Kilometer);
            var journey = new Journey(command.JourneyId, command.DateOfOccurrence, routeDistance, _eventBus);
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
                personId = _idFactory.Create();
                var person = new Person(personId, personName, _eventBus);
                _repositories.Store(person);
            }
            return personId;
        }
    }
}
