using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Journeys.Data
{
    [Repository]
    public interface IDomainRepository<TEntity>
        where TEntity : IHasId<TEntity>
    {
        TEntity Get(Id<TEntity> id);

        void Store(TEntity journey);
    }
}
