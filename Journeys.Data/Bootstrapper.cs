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
            var journeyView = new JourneyView();
            _eventBus.RegisterListener<JourneyCreatedEvent>(journeyView.Update);
            _eventBus.RegisterListener<LiftAddedEvent>(journeyView.Update);

            var queryProcessor = new QueryProcessor();

            queryProcessor.SetHandler<GetAllJourneysWithLiftsQuery, IEnumerable<JourneyWithLift>>(query => journeyView.GetAllJourneysWithLifts());

            QueryDispatcher = new QueryDispatcher(queryProcessor);
        }
    }
}
