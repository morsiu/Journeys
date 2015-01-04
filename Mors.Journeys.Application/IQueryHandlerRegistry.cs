using Mors.Journeys.Common;

namespace Mors.Journeys.Application
{
    public interface IQueryHandlerRegistry
    {
        void SetHandler<TQuery, TResult>(QueryHandler<TQuery, TResult> handler)
            where TQuery : IQuery<TResult>;
    }
}
