using Journeys.Common;

namespace Journeys.Client.Wpf.Events
{
    internal class JourneyWithLiftsAddedEvent
    {
        public IId JourneyId { get; private set; }

        public JourneyWithLiftsAddedEvent(IId journeyId)
        {
            JourneyId = journeyId;
        }
    }
}
