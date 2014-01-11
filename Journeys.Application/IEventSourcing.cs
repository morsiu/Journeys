using System;

namespace Journeys.Application
{
    public interface IEventSourcing
    {
        void RegisterEventReplayer<TEvent>(Action<TEvent> eventReplayer);
    }
}
