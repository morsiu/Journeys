using System.Collections.Generic;
using Journeys.Common;
using Journeys.Transactions;

namespace Journeys.Repositories
{
    internal class TransactedRepository<TEntity> : IRepository<TEntity>, ITransactional<IRepository<TEntity>>
        where TEntity : IHasId
    {
        private readonly Repository<TEntity> _repository;
        private readonly Dictionary<IId, TEntity> _transactionRepository = new Dictionary<IId, TEntity>();

        public TransactedRepository(Repository<TEntity> repository)
        {
            _repository = repository;
        }

        public TEntity Get(IId id)
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
            _transactionRepository.Clear();
        }

        public IRepository<TEntity> Object
        {
            get { return this; }
        }

        public ITransactional<IRepository<TEntity>> Lift()
        {
            return this;
        }
    }
}
