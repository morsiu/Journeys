﻿namespace Mors.Journeys.Common
{
    public delegate TResult QueryHandler<TQuery, TResult>(TQuery query)
        where TQuery : IQuery<TResult>;
}
