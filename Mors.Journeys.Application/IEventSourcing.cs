using System;

namespace Mors.Journeys.Application
{
    public interface IEventSourcing
    {
        void RegisterEventReplayer<TEvent>(Action<TEvent> eventReplayer);
    }
}
