using Mors.AppPlatform.Common.Services;
using Mors.Journeys.Data.Commands;
using Mors.Journeys.Data.Queries;
using Mors.Journeys.Domain.Journeys.Capabilities;
using Mors.Journeys.Domain.Journeys.Operations;
using Mors.Journeys.Domain.People;

namespace Mors.Journeys.Application.CommandHandlers
{
    internal sealed class AddJourneyWithLiftsCommandHandler
    {
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
            _idFactory = idFactory;
            _queryDispatcher = queryDispatcher;
            _repositories = repositories;
            _eventBus = eventBus;
        }

        public void Execute(AddJourneyWithLiftsCommand command)
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
