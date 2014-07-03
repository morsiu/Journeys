using System;
using Journeys.Client.Wpf;
using Mors.Support.Dispatching;

namespace Journeys.Adapters
{
    public class WpfClientQueryHandlerRegistry : IQueryHandlerRegistry
    {
        private readonly HandlerRegistry _handlerRegistry;

        public WpfClientQueryHandlerRegistry(HandlerRegistry handlerRegistry)
        {
            _handlerRegistry = handlerRegistry;
        }

        public void SetHandler<TQuery, TResult>(Client.Wpf.QueryHandler<TQuery, TResult> handler)
            where TQuery : Queries.IQuery<TResult>
        {
            var queryKey = QueryKey.From<TQuery, TResult>();
            Func<object, object> adaptedHandler = query => handler((TQuery)query);
            _handlerRegistry.Set(queryKey, adaptedHandler);
        }
    }
}
