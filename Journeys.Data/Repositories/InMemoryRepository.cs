using Journeys.Data.Exceptions;
using Journeys.Data.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Data.Repositories
{
    public class InMemoryRepository<TId, TEntity>
    {
        private readonly IDictionary<TId, TEntity> _store = new Dictionary<TId, TEntity>();

        public TEntity Get(TId id)
        {
            if (!_store.ContainsKey(id)) 
                throw new EntityNotFoundException(string.Format(FailureMessages.EntityOfTypeWithIdNotFound, typeof(TEntity), id));
            return _store[id];
        }

        private void Store(TId id, TEntity entity)
        {
            _store[id] = entity;
        }
    }
}
