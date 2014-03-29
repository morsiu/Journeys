using System;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;

namespace Journeys.Service.Client
{
    public sealed class QueryRequest<TResult>
    {
        private readonly DataContractSerializer _serializer;
        private readonly Uri _requestUri;
        private readonly object _query;
        private readonly Type _idImplementationType;

        public QueryRequest(Uri requestUri, object query, Type idImplementationType)
        {
            var queryType = query.GetType();
            _requestUri = requestUri;
            _idImplementationType = idImplementationType;
            _query = query;
            _serializer = new DataContractSerializer(typeof(object), new[] { queryType, typeof(TResult), idImplementationType });
        }

        public TResult Run()
        {
            var request = WebRequest.CreateHttp(_requestUri);
            request.Method = "POST";
            request.ContentType = "application/xml";
            var requestStream = request.GetRequestStream();
            _serializer.WriteObject(requestStream, _query);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            var result = (TResult)_serializer.ReadObject(responseStream);
            return result;
        }
    }
}
