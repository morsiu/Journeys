using System;
using System.Collections.Generic;
using Journeys.Data.Messages;
using Journeys.Queries;

namespace Journeys.Query.Infrastructure
{
    internal class QueryProcessor
    {
        private readonly Dictionary<Type, Func<object, object>> _handlers = new Dictionary<Type, Func<object, object>>();

        public void SetHandler<TQuery, TResult>(QueryHandler<TQuery, TResult> handler)
            where TQuery : IQuery<TResult>
        {
            var queryType = typeof(TQuery);
            Func<object, object> untypedHandler = query => handler((TQuery)query);
            _handlers[queryType] = untypedHandler;
        }

        public TResult Handle<TResult>(IQuery<TResult> query)
        {
            if (query == null) throw new ArgumentNullException("query");
            var untypedHandler = GetHandler(query);
            return (TResult)untypedHandler(query);
        }

        private Func<object, object> GetHandler(object query)
        {
            var queryType = query.GetType();
            if (!_handlers.ContainsKey(queryType)) 
                throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForQueryOfType, queryType));
            var untypedHandler = _handlers[queryType];
            return untypedHandler;
        }
    }
}
