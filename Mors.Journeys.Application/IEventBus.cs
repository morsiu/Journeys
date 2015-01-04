using System;

namespace Mors.Journeys.Application
{
    public interface IEventBus
    {
        void RegisterListener<TEvent>(Action<TEvent> handler);
    }
}
