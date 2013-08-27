using Journeys.Transactions;

namespace Journeys.Command
{
    public interface IEventBus : IProvideTransactional<IEventBus>
    {
        Domain.IEventBus ForDomain();
    }
}
