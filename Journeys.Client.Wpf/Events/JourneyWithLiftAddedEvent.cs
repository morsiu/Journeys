using System;

namespace Journeys.Client.Wpf.Events
{
    internal class JourneyWithLiftAddedEvent
    {
        public Guid JourneyId { get; private set; }

        public JourneyWithLiftAddedEvent(Guid journeyId)
        {
            JourneyId = journeyId;
        }
    }
}
