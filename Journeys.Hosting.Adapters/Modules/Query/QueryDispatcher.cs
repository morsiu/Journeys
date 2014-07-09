using Journeys.Data.Queries;
using Journeys.Hosting.Adapters.Dispatching;
using Journeys.Support.Dispatching;

namespace Journeys.Hosting.Adapters.Modules.Query
{
    public class QueryDispatcher : Application.Query.IQueryDispatcher
    {
        private readonly HandlerDispatcher _handlerDispatcher;

        public QueryDispatcher(HandlerDispatcher handlerDispatcher)
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
