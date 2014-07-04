using Journeys.Data.Queries;
using Journeys.Hosting.Adapters.Dispatching;
using Journeys.Hosting.Service.Client;
using System;
using Implementation = Mors.Support.Dispatching;

namespace Journeys.Hosting.Adapters.Modules.WpfClient
{
    public class WpfClientQueryDispatcher : Application.Client.Wpf.IQueryDispatcher
    {
        private readonly Uri _queryRequestUri;
        private readonly Implementation.HandlerDispatcher _handlerDispatcher;

        public WpfClientQueryDispatcher(Uri queryRequestUri, Implementation.HandlerDispatcher handlerDispatcher)
        {
            _queryRequestUri = queryRequestUri;
            _handlerDispatcher = handlerDispatcher;
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();
            if (IsInternal(queryType))
            {
                return DispatchInternal(query);
            }
            else
            {
                return DispatchExternal(query);
            }
        }

        private TResult DispatchExternal<TResult>(IQuery<TResult> query)
        {
            var queryRequest = new QueryRequest<TResult>(_queryRequestUri, query);
            var result = queryRequest.Run();
            return result;
        }

        private TResult DispatchInternal<TResult>(IQuery<TResult> query)
        {
            var queryAdapter = new QueryAdapter<TResult>(query);
            return queryAdapter.Execute(_handlerDispatcher);
        }

        private bool IsInternal(Type queryType)
        {
            return !queryType.IsPublic;
        }
    }
}
