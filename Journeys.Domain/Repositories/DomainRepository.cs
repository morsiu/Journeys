using Journeys.Domain.Infrastructure;
using Journeys.Domain.Journeys.Data;
using Journeys.Transactions;
using System;

namespace Journeys.Domain.Repositories
{
    public class DomainRepository<TEntity> : IDomainRepository<TEntity>, IProvideTransacted<IDomainRepository<TEntity>>
        where TEntity : IHasId<TEntity>
    {
        private readonly InMemoryRepository<Id<TEntity>, TEntity> _repository = new InMemoryRepository<Id<TEntity>, TEntity>();

        public TEntity Get(Id<TEntity> id)
        {
            return _repository.Get(id);
        }

        public void Store(TEntity entity)
        {
            _repository.Store(entity.Id, entity);
        }

        public ITransacted<IDomainRepository<TEntity>> Lift()
        {
            return new TransactedDomainRepository<TEntity>(this);
        }
    }
}
