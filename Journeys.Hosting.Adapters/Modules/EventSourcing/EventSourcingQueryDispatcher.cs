using Journeys.Application.EventSourcing;
using Journeys.Data.Queries;
using Journeys.Hosting.Adapters.Dispatching;
using Journeys.Support.Dispatching;

namespace Journeys.Hosting.Adapters.Modules.EventSourcing
{
    public sealed class EventSourcingQueryDispatcher : IQueryDispatcher
    {
        private readonly HandlerDispatcher _handlerDispatcher;

        public EventSourcingQueryDispatcher(HandlerDispatcher handlerDispatcher)
        {
            _handlerDispatcher = handlerDispatcher;
        }

        public TResult Dispatch<TResult>(IQuery<TResult> querySpecification)
        {
            var query = new Query<TResult>(querySpecification);
            return query.Execute(_handlerDispatcher);
        }
    }
}
