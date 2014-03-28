using Journeys.Client.Wpf;
using Journeys.Dispatching;
using Journeys.Queries;
using Journeys.Service.Client;
using System;

namespace Journeys.Adapters
{
    public class WpfClientQueryDispatcher : IQueryDispatcher
    {
        private readonly Uri _queryRequestUri;
        private readonly HandlerDispatcher _handlerDispatcher;
        private readonly Type _idImplementationType;

        public WpfClientQueryDispatcher(Uri queryRequestUri, HandlerDispatcher handlerDispatcher, Type idImplementationType)
        {
            _queryRequestUri = queryRequestUri;
            _idImplementationType = idImplementationType;
            _handlerDispatcher = handlerDispatcher;
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();
            if (IsInternal(queryType))
            {
                return DispatchInternal<TResult>(query);
            }
            else
            {
                return DispatchExternal<TResult>(query);
            }
        }

        private TResult DispatchExternal<TResult>(IQuery<TResult> query)
        {
            var queryRequest = new QueryRequest<TResult>(_queryRequestUri, query, _idImplementationType);
            var result = queryRequest.Run();
            return result;
        }

        private TResult DispatchInternal<TResult>(IQuery<TResult> query)
        {
            var queryAdapter = new Query<TResult>(query);
            return queryAdapter.Execute(_handlerDispatcher);
        }

        private bool IsInternal(Type queryType)
        {
            return !queryType.IsPublic;
        }
    }
}
