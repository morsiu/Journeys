namespace Journeys.Domain.Infrastructure
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event);
    }
}
