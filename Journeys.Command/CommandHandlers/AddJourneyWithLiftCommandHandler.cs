using System;
using Journeys.Commands;
using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Repositories;
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
        private readonly IDomainRepository<Person> _personRepository;
        private readonly IDomainRepository<Journey> _journeyRepository;

        public AddJourneyWithLiftCommandHandler(
            IEventBus eventBus,
            IDomainRepositories domainRepositories,
            IQueryDispatcher queryDispatcher)
        {
            _transaction = new Transaction();
            _personRepository = _transaction.Add(domainRepositories.Get<Person>());
            _journeyRepository = _transaction.Add(domainRepositories.Get<Journey>());
            _eventBus = _transaction.Add(eventBus);
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
            var journeyId = new Id<Journey>(command.JourneyId);
            var journey = new Journey(journeyId, command.DateOfOccurrence, routeDistance, _eventBus)
                .AddLift(new Id<Person>(personId), liftDistance);
            _journeyRepository.Store(journey);
        }

        private Id<Person> GetOrAddPersonWithName(string personName)
        {
            var personId = _queryDispatcher.Dispatch(new GetPersonIdByNameQuery(personName));
            if (!personId.HasValue)
            {
                personId = Guid.NewGuid();
                var person = new Person(new Id<Person>(personId.Value), personName, _eventBus);
                _personRepository.Store(person);
            }
            return new Id<Person>(personId.Value);
        }
    }
}
