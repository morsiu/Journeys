using Journeys.Client.Wpf;
using Journeys.Dispatching;
using Journeys.Service.Client;
using System;

namespace Journeys.Adapters
{
    public class WpfClientCommandDispatcher : ICommandDispatcher
    {
        private readonly HandlerDispatcher _handlerDispatcher;
        private readonly Uri _commandRequestUri;
        private readonly Type _idImplementationType;

        public WpfClientCommandDispatcher(Uri commandRequestUri, HandlerDispatcher handlerDispatcher, Type idImplementationType)
        {
            _handlerDispatcher = handlerDispatcher;
            _commandRequestUri = commandRequestUri;
            _idImplementationType = idImplementationType;
        }

        public void Dispatch<TCommand>(TCommand command)
        {
            if (IsInternal(typeof(TCommand)))
            {
                DispatchInternal(command);
            }
            else
            {
                DispatchExtrenal(command);
            }
        }

        private bool IsInternal(Type commandType)
        {
            return !commandType.IsPublic;
        }

        private void DispatchExtrenal(object command)
        {
            var request = new CommandRequest(_commandRequestUri, command, _idImplementationType);
            request.Run();
        }

        private void DispatchInternal(object command)
        {
            var commandAdapter = new Adapters.Command(command);
            commandAdapter.Execute(_handlerDispatcher);
        }
    }
}
