using Journeys.Application.Adapters;
using Journeys.Hosting.Service.Infrastructure;
using Nancy;
using System.IO;

namespace Journeys.Hosting.Service.Modules
{
    public class QueryModule : NancyModule
    {
        private readonly ServiceQueryDispatcher _dispatcher;
        private readonly ContentTypeAwareSerializer _serializer = new ContentTypeAwareSerializer();

        public QueryModule(ServiceQueryDispatcher dispatcher)
        {
            _dispatcher = dispatcher;

            Post["/api/query"] = HandleQueryPost;
        }

        private dynamic HandleQueryPost(dynamic parameters)
        {
            var query = DeserializeRequest();
            var result = _dispatcher.Dispatch(query);
            return PrepareResponse(result);
        }

        private object DeserializeRequest()
        {
            var request = _serializer.Deserialize(Request.Body, Request.Headers.ContentType);
            return request;
        }

        private Response PrepareResponse(object result)
        {
            var responseStream = new MemoryStream();
            var responseContentType = _serializer.Serialize(result, responseStream, Request.Headers.Accept);
            responseStream.Seek(0, SeekOrigin.Begin);
            return Response.FromStream(responseStream, responseContentType);
        }
    }
}