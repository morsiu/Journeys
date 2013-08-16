using Journeys.Queries;

namespace Journeys.Query.Infrastructure
{
    internal delegate TResult QueryHandler<TQuery, TResult>(TQuery query)
        where TQuery : IQuery<TResult>;
}
