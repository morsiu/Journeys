using System;
using System.Net;
using System.Runtime.Serialization;

namespace Journeys.Service.Client
{
    public sealed class CommandRequest
    {
        private readonly DataContractSerializer _serializer;
        private readonly Uri _requestUri;
        private readonly object _command;
        private readonly Type _idImplementationType;

        public CommandRequest(Uri requestUri, object command, Type idImplementationType)
        {
            var commandType = command.GetType();
            _requestUri = requestUri;
            _idImplementationType = idImplementationType;
            _command = command;
            _serializer = new DataContractSerializer(typeof(object), new[] { commandType, idImplementationType });
        }

        public void Run()
        {
            var request = WebRequest.CreateHttp(_requestUri);
            request.Method = "POST";
            request.ContentType = "text/plain";
            var requestStream = request.GetRequestStream();
            _serializer.WriteObject(requestStream, _command);
        }
    }
}
