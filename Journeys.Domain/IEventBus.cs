using Journeys.Transactions;

namespace Journeys.Domain
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event);
    }
}
