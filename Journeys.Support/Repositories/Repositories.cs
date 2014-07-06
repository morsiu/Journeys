using System;
using System.Collections.Generic;
using Journeys.Support.Transactions;

namespace Journeys.Support.Repositories
{
    public sealed class Repositories : IRepositories
    {
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public TEntity Get<TEntity>(object id)
        {
            var repository = GetRepository<TEntity>();
            return repository.Get(id);
        }
        
        public void Store<TEntity>(object id, TEntity entity)
        {
            var repository = GetRepository<TEntity>();
            repository.Store(id, entity);
        }

        internal Repository<TEntity> GetRepository<TEntity>()
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
