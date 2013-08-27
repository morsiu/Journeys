using Journeys.Commands;
using Journeys.Common;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using Journeys.Eventing;
using Journeys.Queries;
using Journeys.Query;
using Journeys.Transactions;

namespace Journeys.Command.CommandHandlers
{
    internal class AddJourneyWithLiftCommandHandler
    {
        private readonly Transaction _transaction;
        private readonly IEventBus _eventBus;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IRepositories _repositories;
        private readonly IIdFactory _idFactory;

        public AddJourneyWithLiftCommandHandler(
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

        public void ExecuteTransacted(AddJourneyWithLiftCommand command)
        {
            _transaction.Run(() => Execute(command));
        }

        private void Execute(AddJourneyWithLiftCommand command)
        {
            var routeDistance = new Distance(command.RouteDistance, DistanceUnit.Kilometer);
            var liftDistance = new Distance(command.LiftDistance, DistanceUnit.Kilometer);
            var personId = GetOrAddPersonWithName(command.PersonName);
            var journey = new Journey(command.JourneyId, command.DateOfOccurrence, routeDistance, _eventBus)
                .AddLift(personId, liftDistance);
            _repositories.Store(journey);
        }

        private IId GetOrAddPersonWithName(string personName)
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
