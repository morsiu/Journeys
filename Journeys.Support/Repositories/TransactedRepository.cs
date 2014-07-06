using System.Collections.Generic;
using Journeys.Support.Transactions;

namespace Journeys.Support.Repositories
{
    internal sealed class TransactedRepository<TEntity> : IRepository<TEntity>, ITransactional<IRepository<TEntity>>
    {
        private readonly Repository<TEntity> _repository;
        private readonly Dictionary<object, TEntity> _transactionRepository = new Dictionary<object, TEntity>();

        public TransactedRepository(Repository<TEntity> repository)
        {
            _repository = repository;
        }

        public TEntity Get(object id)
        {
            if (_transactionRepository.ContainsKey(id))
            {
                return _transactionRepository[id];
            }
            return _repository.Get(id);
        }

        public void Store(object id, TEntity entity)
        {
            _transactionRepository[id] = entity;
        }
        
        public void Abort()
        {
            _transactionRepository.Clear();
        }

        public void Commit()
        {
            foreach (var idWithEntity in _transactionRepository)
            {
                var id = idWithEntity.Key;
                var entity = idWithEntity.Value;
                _repository.Store(id, entity);
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
