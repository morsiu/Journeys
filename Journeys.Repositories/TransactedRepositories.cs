using System;
using System.Collections.Generic;
using Journeys.Transactions;
using Journeys.Common;

namespace Journeys.Repositories
{
    using EntityType = Type;

    internal class TransactedRepositories : IRepositories, ITransactional<IRepositories>
    {
        private readonly Dictionary<EntityType, ITransactional> _transactedRepositories = new Dictionary<EntityType, ITransactional>();
        private readonly Repositories _repositories;

        public TransactedRepositories(Repositories repositories)
        {
            _repositories = repositories;
        }

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

        private IRepository<TEntity> GetRepository<TEntity>() where TEntity : IHasId
        {
            var entityType = typeof(TEntity);
            if (_transactedRepositories.ContainsKey(entityType))
            {
                var repository = (IRepository<TEntity>)_transactedRepositories[entityType];
                return repository;
            }
            var newRepository = _repositories.GetRepository<TEntity>();
            var transactedRepository = new TransactedRepository<TEntity>(newRepository);
            _transactedRepositories[entityType] = transactedRepository;
            return newRepository;
        }

        public IRepositories Object
        {
            get { return this; }
        }

        public void Abort()
        {
            foreach (var repository in _transactedRepositories.Values)
            {
                repository.Abort();
            }
        }

        public void Commit()
        {
            foreach (var repository in _transactedRepositories.Values)
            {
                repository.Commit();
            }
        }

        public ITransactional<IRepositories> Lift()
        {
            return this;
        }
    }
}
