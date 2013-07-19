using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Infrastructure.IdGeneration
{
    internal class GuidIdGenerator<TEntity> : IIdGenerator<TEntity>
    {
        public Id<TEntity> GenerateId()
        {
            var guid = Guid.NewGuid();
            return new Id<TEntity>(guid);
        }
    }
}
