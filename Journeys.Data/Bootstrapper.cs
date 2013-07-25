using Journeys.Data.Infrastructure;
using Journeys.Data.Journeys;
using Journeys.Eventing;
using Journeys.Events;
using Journeys.Queries;
using Journeys.Queries.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Data
{
    public class Bootstrapper
    {
        private readonly EventBus _eventBus;

        public Bootstrapper(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public IQueryDispatcher QueryDispatcher { get; private set; }

        public void Bootstrap()
        {
            var queryProcessor = new QueryProcessor();
            var queryDispatcher = new QueryDispatcher(queryProcessor);

            var personView = new PersonView();
            var journeyView = new JourneyView(queryDispatcher);
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeyView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeyView.Update);
            _eventBus.RegisterListener<PersonCreatedEvent>(personView.Update);

            queryProcessor.SetHandler<GetAllJourneysWithLiftsQuery, IEnumerable<JourneyWithLift>>(query => journeyView.GetAllJourneysWithLifts());
            queryProcessor.SetHandler<GetPersonNameQuery, string>(query => personView.GetPersonName(query.PersonId));
            queryProcessor.SetHandler<GetIdOfPersonWithNameQuery, Guid?>(query => personView.GetIdOfPersonWithName(query.PersonName));

            QueryDispatcher = new QueryDispatcher(queryProcessor);
        }
    }
}
