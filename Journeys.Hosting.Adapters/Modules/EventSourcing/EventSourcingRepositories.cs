using Journeys.Application.EventSourcing;
using Journeys.Domain.Infrastructure;
using Journeys.Support.Transactions;
using Implementation = Journeys.Support.Repositories;

namespace Journeys.Hosting.Adapters.Modules.EventSourcing
{
    public sealed class EventSourcingRepositories : IRepositories
    {
        private readonly Implementation.IRepositories _repositories;

        public EventSourcingRepositories(Implementation.IRepositories repositories)
        {
            _repositories = repositories;
        }

        public TEntity Get<TEntity>(object id) where TEntity : IHasId
        {
            return _repositories.Get<TEntity>(id);
        }

        public void Store<TEntity>(TEntity entity) where TEntity : IHasId
        {
            _repositories.Store(entity.Id, entity);
        }

        public ITransactional<IRepositories> Lift()
        {
            return new EventSourcingTransactedRepositories(_repositories);
        }
    }
}
