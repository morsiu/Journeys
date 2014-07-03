namespace Journeys.Application.Client.Wpf.Events
{
    internal class JourneyWithLiftsAddedEvent
    {
        public object JourneyId { get; private set; }

        public JourneyWithLiftsAddedEvent(object journeyId)
        {
            JourneyId = journeyId;
        }
    }
}
