using Journeys.Support.Transactions;

namespace Journeys.Support.Repositories
{
    internal interface IRepository<TEntity> : IProvideTransactional<IRepository<TEntity>>
    {
        TEntity Get(object id);

        void Store(object id, TEntity entity);
    }
}
