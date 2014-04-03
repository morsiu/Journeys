using Journeys.Adapters;
using Nancy;
using Nancy.ModelBinding;
using System.IO;
using System.Runtime.Serialization;

namespace Journeys.Service.Modules
{
    public class QueryModule : NancyModule
    {
        private readonly ServiceQueryDispatcher _dispatcher;
        private readonly NetDataContractSerializer _serializer = new NetDataContractSerializer();

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
            var xmlRequestStream = Request.Body;
            var request = _serializer.Deserialize(xmlRequestStream);
            return request;
        }

        private Response PrepareResponse(object result)
        {
            var responseStream = new MemoryStream();
            _serializer.Serialize(responseStream, result);
            responseStream.Seek(0, SeekOrigin.Begin);
            return Response.FromStream(responseStream, "application/xml");
        }
    }
}