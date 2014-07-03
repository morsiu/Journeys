using Journeys.Application.Adapters;
using Journeys.Application.Service.Infrastructure;
using Nancy;
using System.Runtime.Serialization;

namespace Journeys.Application.Service.Modules
{
    public class CommandModule : NancyModule
    {
        private readonly ServiceCommandDispatcher _dispatcher;
        private readonly ContentTypeAwareSerializer _serializer = new ContentTypeAwareSerializer();

        public CommandModule(ServiceCommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;

            Post["/api/command"] = HandleCommandPost;
        }

        private dynamic HandleCommandPost(dynamic parameters)
        {
            var command = DeserializeRequest();
            _dispatcher.Dispatch(command);
            return PrepareResponse();
        }

        private object DeserializeRequest()
        {
            var request = _serializer.Deserialize(Request.Body, Request.Headers.ContentType);
            return request;
        }

        private Response PrepareResponse()
        {
            return new Response { StatusCode = HttpStatusCode.OK };
        }
    }
}
