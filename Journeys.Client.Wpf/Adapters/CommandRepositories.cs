using Journeys.Command;
using Journeys.Common;
using Journeys.Transactions;

namespace Journeys.Client.Wpf.Adapters
{
    using Source = Journeys.Repositories;

    public class CommandRepositories : IRepositories
    {
        private readonly Source.IRepositories _repositories;

        public CommandRepositories(Source.IRepositories repositories)
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
            return new CommandTransactedRepositories(_repositories);
        }
    }
}
