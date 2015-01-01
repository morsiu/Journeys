using Mors.Journeys.Data;

namespace Mors.Journeys.Application
{
    public interface IRepositories
    {
        TEntity Get<TEntity>(object id) where TEntity : IHasId;

        void Store<TEntity>(TEntity entity) where TEntity : IHasId;
    }
}
