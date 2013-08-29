using System;
using Journeys.Dispatching.Exceptions;
using Journeys.Dispatching.Messages;
using Journeys.Queries;

namespace Journeys.Dispatching
{
    public class QueryProcessor
    {
        private readonly HandlerRegistry _handlers;
        private readonly HandlerDispatcher _dispatcher;

        public QueryProcessor()
        {
            _handlers = new HandlerRegistry();
            _dispatcher = new HandlerDispatcher(_handlers);
        }

        public void SetHandler<TQuery, TResult>(QueryHandler<TQuery, TResult> handler)
            where TQuery : IQuery<TResult>
        {
            var queryType = typeof(TQuery);
            _handlers.Set(queryType, query => handler((TQuery)query));
        }

        public TResult Handle<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();
            try
            {
                return (TResult)_dispatcher.Dispatch(queryType, query);
            }
            catch (HandlerNotFoundException)
            {
                throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForQueryOfType, queryType));
            }
        }
    }
}
