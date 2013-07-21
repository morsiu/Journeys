namespace Journeys.Eventing
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event);
    }
}
