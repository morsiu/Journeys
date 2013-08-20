using System;
using System.Collections.Generic;
using Journeys.Queries;
using Journeys.Query.Messages;

namespace Journeys.Query.Infrastructure
{
    using QueryType = Type;

    internal class QueryProcessor
    {
        private delegate object UntypedQueryHandler(object query);

        private readonly Dictionary<QueryType, UntypedQueryHandler> _handlers = new Dictionary<QueryType, UntypedQueryHandler>();

        public void SetHandler<TQuery, TResult>(QueryHandler<TQuery, TResult> handler)
            where TQuery : IQuery<TResult>
        {
            var queryType = typeof(TQuery);
            UntypedQueryHandler untypedHandler = query => handler((TQuery)query);
            _handlers[queryType] = untypedHandler;
        }

        public TResult Handle<TResult>(IQuery<TResult> query)
        {
            if (query == null) throw new ArgumentNullException("query");
            var untypedHandler = GetHandler(query);
            return (TResult)untypedHandler(query);
        }

        private UntypedQueryHandler GetHandler(object query)
        {
            var queryType = query.GetType();
            if (!_handlers.ContainsKey(queryType)) 
                throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForQueryOfType, queryType));
            var untypedHandler = _handlers[queryType];
            return untypedHandler;
        }
    }
}
