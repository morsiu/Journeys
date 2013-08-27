using Journeys.Queries;

namespace Journeys.Dispatching
{
    public delegate TResult QueryHandler<TQuery, TResult>(TQuery query)
        where TQuery : IQuery<TResult>;
}
