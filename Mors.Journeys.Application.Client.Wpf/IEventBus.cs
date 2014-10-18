using System;

namespace Journeys.Application.Client.Wpf
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event);

        void Subscribe<TEvent>(Action<TEvent> listener);
    }
}
