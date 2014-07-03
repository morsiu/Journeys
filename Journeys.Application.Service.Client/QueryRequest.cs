using System;
using System.Net;
using System.Runtime.Serialization;

namespace Journeys.Application.Service.Client
{
    public sealed class QueryRequest<TResult>
    {
        private readonly NetDataContractSerializer _serializer;
        private readonly Uri _requestUri;
        private readonly object _query;

        public QueryRequest(Uri requestUri, object query)
        {
            var queryType = query.GetType();
            _requestUri = requestUri;
            _query = query;
            _serializer = new NetDataContractSerializer();
        }

        public TResult Run()
        {
            var request = WebRequest.CreateHttp(_requestUri);
            request.Method = "POST";
            request.ContentType = "application/xml";
            request.Accept = "application/xml";
            var requestStream = request.GetRequestStream();
            _serializer.Serialize(requestStream, _query);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            var result = (TResult)_serializer.Deserialize(responseStream);
            return result;
        }
    }
}
