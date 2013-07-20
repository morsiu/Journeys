using Journeys.Domain.Infrastructure;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Journeys.Data
{
    [Repository]
    public interface IDomainRepository<TEntity>
        where TEntity : IHasId
    {
        TEntity Get(Id id);

        void Store(TEntity entity);
    }
}
