namespace Journeys.Domain.Infrastructure.IdGeneration
{
    internal interface IIdGenerator<TEntity>
    {
        Id<TEntity> GenerateId();
    }
}
