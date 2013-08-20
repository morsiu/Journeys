using System;
using System.Collections.Generic;
using Journeys.Transactions;

namespace Journeys.Domain.Infrastructure.Repositories
{
    using EntityType = Type;

    internal class TransactedDomainRepositories : IDomainRepositories, ITransacted<IDomainRepositories>
    {
        private readonly Dictionary<EntityType, ITransactable> _transactedRepositories = new Dictionary<EntityType, ITransactable>();
        private readonly IDomainRepositories _repositories;

        public TransactedDomainRepositories(IDomainRepositories repositories)
        {
            _repositories = repositories;
        }

        public IDomainRepository<TEntity> Get<TEntity>() where TEntity : IHasId<TEntity>
        {
            var entityType = typeof(TEntity);
            if (_transactedRepositories.ContainsKey(entityType))
            {
                var repository = (IDomainRepository<TEntity>)_transactedRepositories[entityType];
                return repository;
            }
            var newRepository = _repositories.Get<TEntity>();
            var transactedRepository = new TransactedDomainRepository<TEntity>(newRepository);
            _transactedRepositories[entityType] = transactedRepository;
            return newRepository;
        }

        public IDomainRepositories Object
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

        public ITransacted<IDomainRepositories> Lift()
        {
            return this;
        }
    }
}
