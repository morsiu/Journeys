using System;
using System.Collections.Concurrent;

namespace Journeys.Support.Dispatching
{
    public sealed class HandlerQueue
    {
        private readonly ConcurrentQueue<Action> _queuedHandlers = new ConcurrentQueue<Action>();

        public void Queue(Action handler)
        {
            _queuedHandlers.Enqueue(handler);
        }

        public bool TryDequeue(out Action handler)
        {
            return _queuedHandlers.TryDequeue(out handler);
        }
    }
}
