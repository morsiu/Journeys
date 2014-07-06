using Journeys.Support.Transactions;

namespace Journeys.Support.Events
{
    public interface IEventBus : IProvideTransactional<IEventBus>
    {
        void RegisterListener<TEvent>(EventListener<TEvent> listener);

        void Publish<TEvent>(TEvent @event);
    }
}
