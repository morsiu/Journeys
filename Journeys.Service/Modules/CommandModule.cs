using Journeys.Adapters;
using Nancy;
using System.Runtime.Serialization;

namespace Journeys.Service.Modules
{
    public class CommandModule : NancyModule
    {
        private readonly ServiceCommandDispatcher _dispatcher;
        private readonly NetDataContractSerializer _serializer = new NetDataContractSerializer();

        public CommandModule(ServiceCommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;

            Post["/api/command"] = HandleCommandPost;
        }

        private dynamic HandleCommandPost(dynamic parameters)
        {
            var query = DeserializeRequest();
            _dispatcher.Dispatch(query);
            return PrepareResponse();
        }

        private object DeserializeRequest()
        {
            var serializedQuery = Request.Body;
            var query = _serializer.Deserialize(serializedQuery);
            return query;
        }

        private Response PrepareResponse()
        {
            return new Response { StatusCode = HttpStatusCode.OK };
        }
    }
}
