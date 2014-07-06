using Journeys.Domain.Infrastructure;
using Journeys.Support.Transactions;

namespace Journeys.Application.Command
{
    public interface IRepositories : IProvideTransactional<IRepositories>
    {
        TEntity Get<TEntity>(object id) where TEntity : IHasId;

        void Store<TEntity>(TEntity entity) where TEntity : IHasId;
    }
}
