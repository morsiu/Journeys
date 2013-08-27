using Journeys.Command;
using Journeys.Dispatching;
using Journeys.Queries;

namespace Journeys.Adapters
{
    public class CommandQueryDispatcher : IQueryDispatcher
    {
        private readonly QueryProcessor _queryProcessor;

        public CommandQueryDispatcher(QueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            return _queryProcessor.Handle(query);
        }
    }
}
