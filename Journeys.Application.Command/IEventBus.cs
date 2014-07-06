using Journeys.Support.Transactions;

namespace Journeys.Application.Command
{
    public interface IEventBus : IProvideTransactional<IEventBus>
    {
        Domain.Infrastructure.IEventBus ForDomain();
    }
}
