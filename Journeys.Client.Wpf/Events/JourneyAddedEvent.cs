using System;

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
