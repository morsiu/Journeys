using System.Collections.Generic;
using Journeys.Support.Repositories.Exceptions;
using Journeys.Support.Repositories.Messages;

namespace Journeys.Support.Repositories
{
    internal sealed class InMemoryRepository<TId, TEntity>
    {
        private readonly IDictionary<TId, TEntity> _store = new Dictionary<TId, TEntity>();

        public TEntity Get(TId id)
        {
            if (!_store.ContainsKey(id)) 
                throw new EntityNotFoundException(string.Format(FailureMessages.EntityOfTypeWithIdNotFound, typeof(TEntity), id));
            return _store[id];
        }

        public void Store(TId id, TEntity entity)
        {
            _store[id] = entity;
        }
    }
}
