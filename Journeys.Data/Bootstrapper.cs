using Journeys.Data.Infrastructure;
using Journeys.Data.Journeys;
using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Events;
using Journeys.Eventing;
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

            queryProcessor.SetHandler<GetJourneysWithLiftsQuery, IEnumerable<JourneyWithLift>>(query => query.Execute(journeyView));

            QueryDispatcher = new QueryDispatcher(queryProcessor);
        }
    }
}
