using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journeys.Common;
using Journeys.Domain;
using Journeys.EventSourcing;
using Journeys.Transactions;

namespace Journeys.Client.Wpf.Adapters
{
    using Source = Journeys.Repositories;

    public class EventSourcingRepositories : IRepositories
    {
        private readonly Source.IRepositories _repositories;

        public EventSourcingRepositories(Source.IRepositories repositories)
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
