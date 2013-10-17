using Journeys.Transactions;

namespace Journeys.Application
{
    public interface IEventBus : IProvideTransactional<IEventBus>
    {
        Domain.Infrastructure.IEventBus ForDomain();
    }
}
