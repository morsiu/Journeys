﻿using Journeys.Transactions;

namespace Journeys.Domain.Infrastructure.Repositories
{
    public class DomainRepository<TEntity> : IDomainRepository<TEntity>
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

        public ITransactional<IDomainRepository<TEntity>> Lift()
        {
            return new TransactedDomainRepository<TEntity>(this);
        }
    }
}
