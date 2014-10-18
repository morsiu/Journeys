using System;
using Journeys.Application.Client.Wpf;
using Journeys.Hosting.Service.Client;
using Journeys.Support.Dispatching;

namespace Journeys.Hosting.Adapters.Modules.WpfClient
{
    public sealed class WpfClientCommandDispatcher : ICommandDispatcher
    {
        private readonly HandlerDispatcher _handlerDispatcher;
        private readonly Uri _commandRequestUri;

        public WpfClientCommandDispatcher(Uri commandRequestUri, HandlerDispatcher handlerDispatcher)
        {
            _handlerDispatcher = handlerDispatcher;
            _commandRequestUri = commandRequestUri;
        }

        public void Dispatch<TCommand>(TCommand command)
        {
            if (IsInternal(typeof(TCommand)))
            {
                DispatchInternal(command);
            }
            else
            {
                DispatchExternal(command);
            }
        }

        private bool IsInternal(Type commandType)
        {
            return !commandType.IsPublic;
        }

        private void DispatchExternal(object command)
        {
            var request = new CommandRequest(_commandRequestUri, command);
            request.Run();
        }

        private void DispatchInternal(object command)
        {
            var commandAdapter = new Dispatching.Command(command);
            commandAdapter.Execute(_handlerDispatcher);
        }
    }
}
