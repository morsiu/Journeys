using System;
using System.Collections.Generic;
using Journeys.Dispatching.Messages;
using Journeys.Queries;

namespace Journeys.Dispatching
{
    using QueryType = Type;

    public class QueryProcessor
    {
        private HandlerRegistry _handlers = new HandlerRegistry();

        public void SetHandler<TQuery, TResult>(QueryHandler<TQuery, TResult> handler)
            where TQuery : IQuery<TResult>
        {
            var queryType = typeof(TQuery);
            _handlers.Set(queryType, query => handler((TQuery)query));
        }

        public TResult Handle<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();
            Func<object, object> handler;
            if (_handlers.Retrieve(queryType, out handler))
            {
                return (TResult)handler(query);
            }
            else
            {
                throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForQueryOfType, queryType));
            }
        }
    }
}
