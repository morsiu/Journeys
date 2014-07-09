using System;
using System.Threading.Tasks;
using Journeys.Support.Dispatching.Exceptions;

namespace Journeys.Support.Dispatching
{
    public sealed class AsyncHandlerDispatcher
    {
        private readonly HandlerQueue _queue;
        private readonly HandlerRegistry _registry;

        public AsyncHandlerDispatcher(HandlerRegistry registry, HandlerQueue queue)
        {
            _registry = registry;
            _queue = queue;
        }

        public Task<object> Dispatch(object key, object parameter)
        {
            Func<object, object> handler;
            if (_registry.Retrieve(key, out handler))
            {
                var resultSource = new TaskCompletionSource<object>();
                _queue.Enqueue(
                    () =>
                    {
                        try
                        {
                            resultSource.SetResult(handler(parameter));
                        }
                        catch (Exception e)
                        {
                            resultSource.SetException(e);
                        }
                    });
                return resultSource.Task;
            }
            else
            {
                throw new HandlerNotFoundException();
            }
        }
    }
}
