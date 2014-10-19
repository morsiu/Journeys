using Mors.AppPlatform.Common;

namespace Mors.Journeys.Application.Client.Wpf
{
    public delegate TResult QueryHandler<TQuery, TResult>(TQuery query)
        where TQuery : IQuery<TResult>;
}
