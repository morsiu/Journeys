using System;

namespace Journeys.Domain.Infrastructure.IdGeneration
{
    internal class GuidIdGenerator<TEntity> : IIdGenerator<TEntity>
    {
        public Id<TEntity> GenerateId()
        {
            var guid = Guid.NewGuid();
            return new Id<TEntity>(guid);
        }
    }
}
