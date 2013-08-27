using Journeys.Common;
using Journeys.EventSourcing;
using Journeys.Transactions;

namespace Journeys.Client.Wpf.Adapters
{
    using Source = Journeys.Repositories;

    internal class EventSourcingTransactedRepositories : IRepositories, ITransactional<IRepositories>
    {
        private readonly ITransactional<Source.IRepositories> _repositories;

        public EventSourcingTransactedRepositories(Source.IRepositories repositories)
        {
            _repositories = repositories.Lift();
        }

        public TEntity Get<TEntity>(IId id) where TEntity : IHasId

        {
            return _repositories.Object.Get<TEntity>(id);
        }

        public void Store<TEntity>(TEntity entity) where TEntity : IHasId
        {
            _repositories.Object.Store(entity);
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
