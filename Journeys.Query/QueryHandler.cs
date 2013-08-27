using Journeys.Queries;

namespace Journeys.Query
{
    public delegate TResult QueryHandler<TQuery, TResult>(TQuery query)
        where TQuery : IQuery<TResult>;
}
