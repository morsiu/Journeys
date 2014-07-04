using Journeys.Data.Queries;
using Journeys.Hosting.Adapters.Dispatching;
using Mors.Support.Dispatching;

namespace Journeys.Hosting.Adapters.Modules.Query
{
    public class QueryDispatcher : Application.Query.IQueryDispatcher
    {
        private readonly HandlerDispatcher _handlerDispatcher;

        public QueryDispatcher(HandlerDispatcher handlerDispatcher)
        {
            _handlerDispatcher = handlerDispatcher;
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            var queryAdapter = new QueryAdapter<TResult>(query);
            return queryAdapter.Execute(_handlerDispatcher);
        }
    }
}
