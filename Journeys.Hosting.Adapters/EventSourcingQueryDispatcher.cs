using Journeys.Application.EventSourcing;
using Journeys.Data.Queries;
using Mors.Support.Dispatching;

namespace Journeys.Application.Adapters
{
    public class EventSourcingQueryDispatcher : IQueryDispatcher
    {
        private readonly HandlerDispatcher _handlerDispatcher;

        public EventSourcingQueryDispatcher(HandlerDispatcher handlerDispatcher)
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
