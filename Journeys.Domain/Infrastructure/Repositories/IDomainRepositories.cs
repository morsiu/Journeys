using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journeys.Transactions;

namespace Journeys.Domain.Infrastructure.Repositories
{
    public interface IDomainRepositories : IProvideTransacted<IDomainRepositories>
    {
        IDomainRepository<TEntity> Get<TEntity>() 
            where TEntity : IHasId<TEntity>;
    }
}
