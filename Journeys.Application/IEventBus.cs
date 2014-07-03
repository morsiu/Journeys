using Mors.Support.Transactions;

namespace Journeys.Application
{
    public interface IEventBus : IProvideTransactional<IEventBus>
    {
        Domain.Infrastructure.IEventBus ForDomain();
    }
}
