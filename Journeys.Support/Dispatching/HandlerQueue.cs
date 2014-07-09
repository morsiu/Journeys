using System;
using System.Collections.Generic;
using System.Threading;

namespace Journeys.Support.Dispatching
{
    public sealed class HandlerQueue
    {
        private readonly Queue<Action> _queuedHandlers = new Queue<Action>();
        private readonly ManualResetEvent _nonEmptyQueueEvent = new ManualResetEvent(false);
        private readonly object _accessLock = new object();

        public WaitHandle WaitHandle
        {
            get { return _nonEmptyQueueEvent; }
        }

        public void Queue(Action handler)
        {
            lock (_accessLock)
            {
                _queuedHandlers.Enqueue(handler);
                _nonEmptyQueueEvent.Set();
            }
        }

        public bool TryDequeue(out Action handler)
        {
            lock (_accessLock)
            {
                if (_queuedHandlers.Count == 0)
                {
                    handler = null;
                    return false;
                }
                if (_queuedHandlers.Count == 1)
                {
                    _nonEmptyQueueEvent.Reset();
                }
                handler = _queuedHandlers.Dequeue();
                return true;
            }
        }
    }
}
