using Journeys.Application;
using Journeys.Domain.Infrastructure;
using Mors.Support.Transactions;
using Implementation = Mors.Support.Repositories;

namespace Journeys.Adapters
{
    public class ApplicationRepositories : IRepositories
    {
        private readonly Implementation.IRepositories _repositories;

        public ApplicationRepositories(Implementation.IRepositories repositories)
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
            return new ApplicationTransactedRepositories(_repositories);
        }
    }
}
