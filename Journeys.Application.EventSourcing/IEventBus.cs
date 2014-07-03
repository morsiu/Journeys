using Mors.Support.Transactions;

namespace Journeys.Application.EventSourcing
{
    public interface IEventBus : IProvideTransactional<IEventBus>
    {
        Domain.Infrastructure.IEventBus ForDomain();
    }
}
