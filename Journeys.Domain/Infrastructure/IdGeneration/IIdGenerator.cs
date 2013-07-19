using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Infrastructure.IdGeneration
{
    internal interface IIdGenerator<TEntity>
    {
        Id<TEntity> GenerateId();
    }
}
