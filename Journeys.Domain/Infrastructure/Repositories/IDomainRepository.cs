using Journeys.Domain.Infrastructure.Markers;
using Journeys.Transactions;

namespace Journeys.Domain.Infrastructure.Repositories
{
    [Repository]
    public interface IDomainRepository<TEntity> : IProvideTransactional<IDomainRepository<TEntity>>
        where TEntity : IHasId<TEntity>
    {
        TEntity Get(Id<TEntity> id);

        void Store(TEntity entity);
    }
}
