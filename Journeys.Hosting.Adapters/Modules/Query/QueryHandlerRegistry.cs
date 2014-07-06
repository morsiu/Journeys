using System;
using Journeys.Data.Queries;
using Journeys.Application.Query;
using Implementation = Journeys.Support.Dispatching;
using Journeys.Hosting.Adapters.Dispatching;

namespace Journeys.Hosting.Adapters.Modules.Query
{
    public class QueryHandlerRegistry : IQueryHandlerRegistry
    {
        private readonly Implementation.HandlerRegistry _handlerRegistry;

        public QueryHandlerRegistry(Implementation.HandlerRegistry handlerRegistry)
        {
            _handlerRegistry = handlerRegistry;
        }

        public void SetHandler<TQuery, TResult>(QueryHandler<TQuery, TResult> handler)
            where TQuery : IQuery<TResult>
        {
            var queryKey = QueryKey.From<TQuery, TResult>();
            Func<object, object> adaptedHandler = query => handler((TQuery)query);
            _handlerRegistry.Set(queryKey, adaptedHandler);
        }
    }
}
