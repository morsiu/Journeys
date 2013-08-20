using System.Collections.Generic;
using Journeys.Transactions;

namespace Journeys.Domain.Infrastructure.Repositories
{
    internal class TransactedDomainRepository<TEntity> : IDomainRepository<TEntity>, ITransacted<IDomainRepository<TEntity>>
        where TEntity : IHasId<TEntity>
    {
        private readonly IDomainRepository<TEntity> _repository;
        private readonly Dictionary<Id<TEntity>, TEntity> _transactionRepository = new Dictionary<Id<TEntity>, TEntity>();

        public TransactedDomainRepository(IDomainRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public TEntity Get(Id<TEntity> id)
        {
            if (_transactionRepository.ContainsKey(id))
            {
                return _transactionRepository[id];
            }
            return _repository.Get(id);
        }

        public void Store(TEntity entity)
        {
            _transactionRepository[entity.Id] = entity;
        }
        
        public void Abort()
        {
            _transactionRepository.Clear();
        }

        public void Commit()
        {
            foreach (var entity in _transactionRepository.Values)
            {
                _repository.Store(entity);
            }
        }

        public IDomainRepository<TEntity> Object
        {
            get { return this; }
        }

        public ITransacted<IDomainRepository<TEntity>> Lift()
        {
            return this;
        }
    }
}
