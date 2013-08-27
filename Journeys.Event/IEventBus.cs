using Journeys.Transactions;

namespace Journeys.Event
{
    public interface IEventBus : IProvideTransactional<IEventBus>
    {
        void RegisterListener<TEvent>(EventListener<TEvent> listener);

        void Publish<TEvent>(TEvent @event);
    }
}
