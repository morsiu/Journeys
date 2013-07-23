namespace Journeys.Domain.Infrastructure
{
    public interface IHasId<TEntity>
    {
        Id<TEntity> Id { get;  }
    }
}
