using System;
using Mors.Journeys.Data;

namespace Mors.Journeys.Application
{
    public interface IQueryHandlerRegistry
    {
        void SetHandler<TQuery, TResult>(Func<TQuery, TResult> handler)
            where TQuery : IQuery<TResult>;
    }
}
