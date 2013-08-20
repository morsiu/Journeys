using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Infrastructure.Repositories
{
    public interface IDomainRepositories
    {
        DomainRepository<TEntity> Get<TEntity>() 
            where TEntity : IHasId<TEntity>;
    }
}
