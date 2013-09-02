using Journeys.Client.Wpf;
using Journeys.Dispatching;
using Journeys.Queries;

namespace Journeys.Adapters
{
    public class WpfClientQueryDispatcher : IQueryDispatcher
    {
        private readonly HandlerDispatcher _handlerDispatcher;

        public WpfClientQueryDispatcher(HandlerDispatcher handlerDispatcher)
        {
            _handlerDispatcher = handlerDispatcher;
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            var queryAdapter = new Query<TResult>(query);
            return queryAdapter.Execute(_handlerDispatcher);
        }
    }
}
