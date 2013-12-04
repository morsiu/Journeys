﻿using Journeys.Common;
using Journeys.Transactions;

namespace Journeys.Application
{
    public interface IRepositories : IProvideTransactional<IRepositories>
    {
        TEntity Get<TEntity>(IId id) where TEntity : IHasId;

        void Store<TEntity>(TEntity entity) where TEntity : IHasId;
    }
}