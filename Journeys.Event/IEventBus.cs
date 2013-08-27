using Journeys.Transactions;

namespace Journeys.Event
{
    public interface IEventBus : IProvideTransactional<IEventBus>
    {
        void Publish<TEvent>(TEvent @event);
    }
}
