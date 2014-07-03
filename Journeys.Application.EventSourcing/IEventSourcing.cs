using System;

namespace Journeys.Application.EventSourcing
{
    public interface IEventSourcing
    {
        void RegisterEventReplayer<TEvent>(Action<TEvent> eventReplayer);
    }
}
