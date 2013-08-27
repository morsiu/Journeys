using Journeys.Queries;

namespace Journeys.Query
{
    public interface IQueryHandlerRegistry
    {
        void SetHandler<TQuery, TResult>(QueryHandler<TQuery, TResult> handler)
            where TQuery : IQuery<TResult>;
    }
}
