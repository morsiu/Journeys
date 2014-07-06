using Journeys.Application.EventSourcing;
using Journeys.Domain.Infrastructure;
using Journeys.Support.Transactions;
using Implementation = Journeys.Support.Repositories;

namespace Journeys.Hosting.Adapters.Modules.EventSourcing
{
    internal class EventSourcingTransactedRepositories : IRepositories, ITransactional<IRepositories>
    {
        private readonly ITransactional<Implementation.IRepositories> _repositories;

        public EventSourcingTransactedRepositories(Implementation.IRepositories repositories)
        {
            _repositories = repositories.Lift();
        }

        public TEntity Get<TEntity>(object id) where TEntity : IHasId
        {
            return _repositories.Object.Get<TEntity>(id);
        }

        public void Store<TEntity>(TEntity entity) where TEntity : IHasId
        {
            _repositories.Object.Store(entity.Id, entity);
        }

        public ITransactional<IRepositories> Lift()
        {
            return this;
        }

        public IRepositories Object
        {
            get { return this; }
        }

        public void Abort()
        {
            _repositories.Abort();
        }

        public void Commit()
        {
            _repositories.Commit();
        }
    }
}
