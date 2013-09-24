using Journeys.Common;
using Journeys.Transactions;

namespace Journeys.Repositories
{
    internal interface IRepository<TEntity> : IProvideTransactional<IRepository<TEntity>>
        where TEntity : IHasId
    {
        TEntity Get(IId id);

        void Store(TEntity entity);
    }
}
