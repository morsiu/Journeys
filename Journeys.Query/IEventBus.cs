using System;

namespace Journeys.Query
{
    public interface IEventBus
    {
        void RegisterListener<TEvent>(Action<TEvent> handler);
    }
}
