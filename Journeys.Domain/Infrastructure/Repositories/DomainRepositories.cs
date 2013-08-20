using System;
using System.Collections.Generic;
using Journeys.Transactions;

namespace Journeys.Domain.Infrastructure.Repositories
{
    internal class DomainRepositories : IDomainRepositories
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public IDomainRepository<TEntity> Get<TEntity>()
            where TEntity : IHasId<TEntity>
        {
            var entityType = typeof(TEntity);
            if (_repositories.ContainsKey(entityType))
            {
                var repository = (DomainRepository<TEntity>)_repositories[entityType];
                return repository;
            }
            var newRepository = new DomainRepository<TEntity>();
            _repositories[entityType] = newRepository;
            return newRepository;
        }

        public ITransactional<IDomainRepositories> Lift()
        {
            return new TransactedDomainRepositories(this);
        }
    }
}
