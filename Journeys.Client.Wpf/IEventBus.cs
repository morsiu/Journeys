using System;

namespace Journeys.Client.Wpf
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event);

        void Subscribe<TEvent>(Action<TEvent> listener);
    }
}
