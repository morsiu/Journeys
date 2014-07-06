using Journeys.Support.Transactions;

namespace Journeys.Support.Repositories
{
    public interface IRepositories : IProvideTransactional<IRepositories>
    {
        TEntity Get<TEntity>(object id);

        void Store<TEntity>(object id, TEntity entity);
    }
}
