using Journeys.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Data.Infrastructure
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly QueryProcessor _queryProcessor;

        internal QueryDispatcher(QueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            return _queryProcessor.Handle<TResult>(query);
        }
    }
}
