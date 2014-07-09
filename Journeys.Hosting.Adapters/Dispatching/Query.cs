using System;
using Journeys.Data.Queries;
using Journeys.Hosting.Adapters.Messages;
using Journeys.Support.Dispatching;
using Journeys.Support.Dispatching.Exceptions;

namespace Journeys.Hosting.Adapters.Dispatching
{
    public class Query<TResult>
    {
        private IQuery<TResult> _querySpecification;

        public Query(IQuery<TResult> querySpecification)
        {
            _querySpecification = querySpecification;
        }

        public TResult Execute(HandlerDispatcher dispatcher)
        {
            var queryKey = QueryKey.From(_querySpecification);
            try
            {
                return (TResult)dispatcher.Dispatch(queryKey, _querySpecification);
            }
            catch (HandlerNotFoundException)
            {
                throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForQueryOfType, queryKey));
            }
        }
    }
}
