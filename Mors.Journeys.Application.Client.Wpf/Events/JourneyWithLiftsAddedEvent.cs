namespace Mors.Journeys.Application.Client.Wpf.Events
{
    internal sealed class JourneyWithLiftsAddedEvent
    {
        public object JourneyId { get; private set; }

        public JourneyWithLiftsAddedEvent(object journeyId)
        {
            JourneyId = journeyId;
        }
    }
}
