using Journeys.Common;
using Journeys.EventSourcing;
using Journeys.Transactions;

namespace Journeys.Adapters
{
    public class EventSourcingRepositories : IRepositories
    {
        private readonly Repositories.IRepositories _repositories;

        public EventSourcingRepositories(Repositories.IRepositories repositories)
        {
            _repositories = repositories;
        }

        public TEntity Get<TEntity>(IId id) where TEntity : IHasId
        {
            return _repositories.Get<TEntity>(id);
        }

        public void Store<TEntity>(TEntity entity) where TEntity : IHasId
        {
            _repositories.Store(entity);
        }

        public ITransactional<IRepositories> Lift()
        {
            return new EventSourcingTransactedRepositories(_repositories);
        }
    }
}
