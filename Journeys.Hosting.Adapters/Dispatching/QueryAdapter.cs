using System;
using Journeys.Data.Queries;
using Journeys.Hosting.Adapters.Messages;
using Mors.Support.Dispatching;
using Mors.Support.Dispatching.Exceptions;

namespace Journeys.Hosting.Adapters.Dispatching
{
    public class QueryAdapter<TResult>
    {
        private IQuery<TResult> _query;

        public QueryAdapter(IQuery<TResult> query)
        {
            _query = query;
        }

        public TResult Execute(HandlerDispatcher dispatcher)
        {
            var queryKey = QueryKey.From(_query);
            try
            {
                return (TResult)dispatcher.Dispatch(queryKey, _query);
            }
            catch (HandlerNotFoundException)
            {
                throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForQueryOfType, queryKey));
            }
        }
    }
}
