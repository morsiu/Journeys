using System;
using Journeys.Dispatching;
using Journeys.Query;

namespace Journeys.Adapters
{
    public class QueryHandlerRegistry : IQueryHandlerRegistry
    {
        private readonly HandlerRegistry _handlerRegistry;

        public QueryHandlerRegistry(HandlerRegistry handlerRegistry)
        {
            _handlerRegistry = handlerRegistry;
        }

        public void SetHandler<TQuery, TResult>(Query.QueryHandler<TQuery, TResult> handler)
            where TQuery : Queries.IQuery<TResult>
        {
            var queryKey = QueryKey.From<TQuery, TResult>();
            Func<object, object> adaptedHandler = query => handler((TQuery)query);
            _handlerRegistry.Set(queryKey, adaptedHandler);
        }
    }
}
