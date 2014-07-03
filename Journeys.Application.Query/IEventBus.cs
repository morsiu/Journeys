using System;

namespace Journeys.Application.Query
{
    public interface IEventBus
    {
        void RegisterListener<TEvent>(Action<TEvent> handler);
    }
}
