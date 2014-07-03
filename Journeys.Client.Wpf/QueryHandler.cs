using Journeys.Data.Queries;

namespace Journeys.Client.Wpf
{
    public delegate TResult QueryHandler<TQuery, TResult>(TQuery query)
        where TQuery : IQuery<TResult>;
}
