using Journeys.Application.Exceptions;
using Journeys.Application.Messages;
using System.Collections.Generic;

namespace Journeys.Application.Repositories
{
    internal class InMemoryRepository<TId, TEntity>
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
