using Journeys.Common;
using Journeys.Transactions;

namespace Journeys.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity>
        where TEntity : IHasId
    {
        private readonly InMemoryRepository<IId, TEntity> _repository = new InMemoryRepository<IId, TEntity>();

        public TEntity Get(IId id)
        {
            return _repository.Get(id);
        }

        public void Store(TEntity entity)
        {
            _repository.Store(entity.Id, entity);
        }

        public ITransactional<IRepository<TEntity>> Lift()
        {
            return new TransactedRepository<TEntity>(this);
        }
    }
}
