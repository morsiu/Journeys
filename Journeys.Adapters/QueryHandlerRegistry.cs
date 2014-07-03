using System;
using Journeys.Query;
using Implementation = Mors.Support.Dispatching;

namespace Journeys.Adapters
{
    public class QueryHandlerRegistry : IQueryHandlerRegistry
    {
        private readonly Implementation.HandlerRegistry _handlerRegistry;

        public QueryHandlerRegistry(Implementation.HandlerRegistry handlerRegistry)
        {
            _handlerRegistry = handlerRegistry;
        }

        public void SetHandler<TQuery, TResult>(QueryHandler<TQuery, TResult> handler)
            where TQuery : Queries.IQuery<TResult>
        {
            var queryKey = QueryKey.From<TQuery, TResult>();
            Func<object, object> adaptedHandler = query => handler((TQuery)query);
            _handlerRegistry.Set(queryKey, adaptedHandler);
        }
    }
}
