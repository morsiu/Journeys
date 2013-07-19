namespace Journeys.Application.Infrastructure.Events
{
    internal delegate void EventListener<TEvent>(TEvent @event);
}
