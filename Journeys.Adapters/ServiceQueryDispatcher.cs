using Journeys.Dispatching;

namespace Journeys.Adapters
{
    public class ServiceQueryDispatcher
    {
        private readonly HandlerDispatcher _handlerDispatcher;

        public ServiceQueryDispatcher(HandlerDispatcher handlerDispatcher)
        {
            _handlerDispatcher = handlerDispatcher;
        }

        public object Dispatch(object query)
        {
            var key = new QueryKey(query.GetType());
            return _handlerDispatcher.Dispatch(key, query);
        }
    }
}
