using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Client.Wpf.Events
{
    internal class JourneyAddedEvent
    {
        public Guid JourneyId { get; private set; }

        public JourneyAddedEvent(Guid journeyId)
        {
            JourneyId = journeyId;
        }
    }
}
