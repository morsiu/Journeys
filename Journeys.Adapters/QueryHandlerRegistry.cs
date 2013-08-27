using Journeys.Dispatching;
using Journeys.Query;

namespace Journeys.Adapters
{
    public class QueryHandlerRegistry : IQueryHandlerRegistry
    {
        private readonly QueryProcessor _queryProcessor;

        public QueryHandlerRegistry(QueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public void SetHandler<TQuery, TResult>(Query.QueryHandler<TQuery, TResult> handler) where TQuery : Queries.IQuery<TResult>
        {
            _queryProcessor.SetHandler(new Dispatching.QueryHandler<TQuery, TResult>(handler));
        }
    }
}
