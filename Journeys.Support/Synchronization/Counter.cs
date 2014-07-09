using System.Threading;

namespace Journeys.Support.Synchronization
{
    public sealed class Counter
    {
        private int _count;
        private EventWaitHandle _event;

        public Counter(int initialCount)
        {
            _count = initialCount;
            _event = new ManualResetEvent(true);
        }

        public void Increase()
        {
            if (Interlocked.Increment(ref _count) == 1)
            {
                _event.Reset();
            }
        }

        public void Decrease()
        {
            if (Interlocked.Decrement(ref _count) == 0)
            {
                _event.Set();
            }
        }

        public void Wait()
        {
            _event.WaitOne();
        }
    }
}
