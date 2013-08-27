using System;
using System.Collections.Generic;
using Journeys.Common;
using Journeys.Transactions;

namespace Journeys.Repositories
{
    public class Repositories : IRepositories
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public TEntity Get<TEntity>(IId id) where TEntity : IHasId
        {
            var repository = GetRepository<TEntity>();
            return repository.Get(id);
        }
        
        public void Store<TEntity>(TEntity entity) where TEntity : IHasId
        {
            var repository = GetRepository<TEntity>();
            repository.Store(entity);
        }

        internal Repository<TEntity> GetRepository<TEntity>()
            where TEntity : IHasId
        {
            var entityType = typeof(TEntity);
            if (_repositories.ContainsKey(entityType))
            {
                var repository = (Repository<TEntity>)_repositories[entityType];
                return repository;
            }
            var newRepository = new Repository<TEntity>();
            _repositories[entityType] = newRepository;
            return newRepository;
        }

        public ITransactional<IRepositories> Lift()
        {
            return new TransactedRepositories(this);
        }
    }
}
