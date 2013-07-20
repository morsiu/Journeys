using Journeys.Data.Repositories;
using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Data;

namespace Journeys.Application.Infrastructure
{
    internal class DomainRepository<TEntity> : IDomainRepository<TEntity>
        where TEntity : IHasId
    {
        private readonly InMemoryRepository<Id, TEntity> _repository = new InMemoryRepository<Id, TEntity>();
        public TEntity Get(Id id)
        {
            return _repository.Get(id);
        }

        public void Store(TEntity entity)
        {
            _repository.Store(entity.Id, entity);
        }
    }
}
