using Journeys.Application;
using Journeys.Domain.Infrastructure;
using Mors.Support.Transactions;
using Implementation = Mors.Support.Repositories;

namespace Journeys.Application.Adapters
{
    internal class ApplicationTransactedRepositories : IRepositories, ITransactional<IRepositories>
    {
        private readonly ITransactional<Implementation.IRepositories> _repositories;

        public ApplicationTransactedRepositories(Implementation.IRepositories repositories)
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
