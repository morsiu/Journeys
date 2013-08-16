using System;
using System.Collections.Generic;

namespace Journeys.Domain.Infrastructure.Repositories
{
    public class DomainRepositories
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public DomainRepository<TEntity> Get<TEntity>()
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
    }
}
