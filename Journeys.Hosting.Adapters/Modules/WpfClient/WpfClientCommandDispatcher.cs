using Journeys.Application.Client.Wpf;
using Journeys.Hosting.Service.Client;
using System;
using Journeys.Support.Dispatching;
using Journeys.Hosting.Adapters.Dispatching;

namespace Journeys.Hosting.Adapters.Modules.WpfClient
{
    public class WpfClientCommandDispatcher : ICommandDispatcher
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
            var commandAdapter = new CommandAdapter(command);
            commandAdapter.Execute(_handlerDispatcher);
        }
    }
}
