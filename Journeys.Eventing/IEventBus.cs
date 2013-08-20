using Journeys.Transactions;

namespace Journeys.Eventing
{
    public interface IEventBus : IProvideTransactional<IEventBus>
    {
        void Publish<TEvent>(TEvent @event);
    }
}
