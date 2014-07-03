using Journeys.Data.Commands;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Operations;
using Journeys.Domain.People;
using Journeys.Data.Queries;
using Mors.Support.Transactions;

namespace Journeys.Application.Command.Handlers
{
    internal class AddJourneyWithLiftsCommandHandler
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
            var journey = new Journey(command.JourneyId, command.DateOfOccurrence, routeDistance, _eventBus.ForDomain());
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
                var person = new Person(personId, personName, _eventBus.ForDomain());
                _repositories.Store(person);
            }
            return personId;
        }
    }
}
