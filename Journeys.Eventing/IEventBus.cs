using Journeys.Transactions;

namespace Journeys.Eventing
{
    public interface IEventBus : IProvideTransacted<IEventBus>
    {
        void Publish<TEvent>(TEvent @event);
    }
}
