using System;
using Journeys.Common;

namespace Journeys.Client.Wpf.Events
{
    internal class JourneyWithLiftAddedEvent
    {
        public IId JourneyId { get; private set; }

        public JourneyWithLiftAddedEvent(IId journeyId)
        {
            JourneyId = journeyId;
        }
    }
}
