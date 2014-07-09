using System;
using System.Threading.Tasks;
using Journeys.Hosting.Adapters.Messages;
using Journeys.Support.Dispatching;

namespace Journeys.Hosting.Adapters.Dispatching
{
    public sealed class AsyncQuery
    {
        private readonly object _querySpecification;
        private readonly HandlerRegistry _registry;

        public AsyncQuery(object querySpecification, HandlerRegistry registry)
        {
            _querySpecification = querySpecification;
            _registry = registry;
        }

        public Task<object> Execute(HandlerQueue queue)
        {
            var handler = FindHandler();
            var resultSource = new TaskCompletionSource<object>();
            queue.Enqueue(
                () =>
                {
                    try
                    {
                        resultSource.SetResult(handler(_querySpecification));
                    }
                    catch (Exception e)
                    {
                        resultSource.SetException(e);
                    }
                });
            return resultSource.Task;
        }

        private Func<object, object> FindHandler()
        {
            var queryKey = new QueryKey(_querySpecification.GetType());
            Func<object, object> handler;
            if (_registry.Retrieve(queryKey, out handler))
            {
                return handler;
            }
            throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForQueryOfType, queryKey));
        }
    }
}
