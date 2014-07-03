using Journeys.Domain.Infrastructure;
using Mors.Support.Transactions;

namespace Journeys.Application.EventSourcing
{
    public interface IRepositories : IProvideTransactional<IRepositories>
    {
        TEntity Get<TEntity>(object id) where TEntity : IHasId;

        void Store<TEntity>(TEntity entity) where TEntity : IHasId;
    }
}
