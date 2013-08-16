using Journeys.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Query.Infrastructure
{
    internal delegate TResult QueryHandler<TQuery, TResult>(TQuery query)
        where TQuery : IQuery<TResult>;
}
