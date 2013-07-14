using Journeys.Domain.Infrastructure.IdGeneration;
using Journeys.Domain.Infrastructure.Markers;
using Journeys.Domain.Journeys.Capabilities;
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

        public Journey Create(DateTime dateOfOccurence, Distance routeDistance)
        {
            var id = _idGenerator.GetNext();
            return new Journey(id, dateOfOccurence, routeDistance);
        }
    }
}
