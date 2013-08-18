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

namespace Journeys.Command.CommandHandlers
{
    internal class AddJourneyWithLiftCommandHandler
    {
        private readonly IEventBus _eventBus;
        private readonly IDomainRepository<Person> _personRepository;
        private readonly IQueryDispatcher _queryDispatcher;

        public AddJourneyWithLiftCommandHandler(
            IEventBus eventBus,
            IDomainRepository<Person> personRepository,
            IQueryDispatcher queryDispatcher)
        {
            _personRepository = personRepository;
            _queryDispatcher = queryDispatcher;
            _eventBus = eventBus;
        }

        public void Execute(
            AddJourneyWithLiftCommand command,
            IDomainRepository<Journey> journeyRepository)
        {
            var routeDistance = new Distance(command.RouteDistance, DistanceUnit.Kilometer);
            var liftDistance = new Distance(command.LiftDistance, DistanceUnit.Kilometer);
            var personId = GetOrAddPersonWithName(command.PersonName);
            var journeyId = new Id<Journey>(command.JourneyId);
            var journey = new Journey(journeyId, command.DateOfOccurrence, routeDistance, _eventBus)
                .AddLift(new Id<Person>(personId), liftDistance);
            journeyRepository.Store(journey);
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
