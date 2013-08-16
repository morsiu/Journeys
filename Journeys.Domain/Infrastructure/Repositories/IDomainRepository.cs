using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Infrastructure.Repositories
{
    [Repository]
    public interface IDomainRepository<TEntity>
        where TEntity : IHasId<TEntity>
    {
        TEntity Get(Id<TEntity> id);

        void Store(TEntity entity);
    }
}
