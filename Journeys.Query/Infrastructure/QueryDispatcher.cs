using Journeys.Queries;

namespace Journeys.Query.Infrastructure
{
    internal class QueryDispatcher : IQueryDispatcher
    {
        private readonly QueryProcessor _queryProcessor;

        internal QueryDispatcher(QueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            return _queryProcessor.Handle(query);
        }
    }
}
