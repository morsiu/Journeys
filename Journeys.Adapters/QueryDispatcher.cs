using Journeys.Dispatching;
using Journeys.Queries;
using Journeys.Query;

namespace Journeys.Adapters
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly QueryProcessor _queryProcessor;

        public QueryDispatcher(QueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            return _queryProcessor.Handle(query);
        }
    }
}
