using Journeys.Data.Queries;
using Mors.Support.Dispatching;

namespace Journeys.Application.Adapters
{
    public class QueryDispatcher : Query.IQueryDispatcher
    {
        private readonly HandlerDispatcher _handlerDispatcher;

        public QueryDispatcher(HandlerDispatcher handlerDispatcher)
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
