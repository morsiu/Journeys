using System;
using System.Threading.Tasks;
using Journeys.Hosting.Adapters.Messages;
using Journeys.Support.Dispatching;
using Journeys.Support.Dispatching.Exceptions;

namespace Journeys.Hosting.Adapters.Dispatching
{
    public sealed class AsyncQuery
    {
        private readonly object _querySpecification;

        public AsyncQuery(object querySpecification)
        {
            _querySpecification = querySpecification;
        }

        public Task<object> Execute(AsyncHandlerDispatcher dispatcher)
        {
            var queryKey = new QueryKey(_querySpecification.GetType());
            try
            {
                return dispatcher.Dispatch(queryKey, _querySpecification);
            }
            catch (HandlerNotFoundException)
            {
                throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForQueryOfType, queryKey));
            }
        }
    }
}
