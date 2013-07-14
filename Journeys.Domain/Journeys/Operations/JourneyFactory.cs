using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.IdGeneration;
using Journeys.Domain.Infrastructure.Markers;
using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Journeys.Operations
{
    [Factory]
    public class JourneyFactory
    {
        private IIdGenerator<Journey> _idGenerator = new GuidIdGenerator<Journey>();
        private IEventBus _eventBus;

        public JourneyFactory(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Journey Create(DateTime dateOfOccurence, Distance routeDistance)
        {
            var id = _idGenerator.GetNext();
            var journey = new Journey(id, dateOfOccurence, routeDistance, _eventBus);
            _eventBus.Publish<JourneyCreatedEvent>(new JourneyCreatedEvent(id, dateOfOccurence, routeDistance));
            return journey;
        }
    }
}
