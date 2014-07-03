using System;
using Mors.Support.Dispatching;

namespace Journeys.Application.Adapters
{
    public class WpfClientCommandHandlerRegistry : Client.Wpf.ICommandHandlerRegistry
    {
        private readonly HandlerRegistry _handlerRegistry;

        public WpfClientCommandHandlerRegistry(HandlerRegistry handlerRegistry)
        {
            _handlerRegistry = handlerRegistry;
        }

        public void SetHandler<TCommand>(Action<TCommand> handler)
        {
            var commandKey = CommandKey.From<TCommand>();
            _handlerRegistry.Set(commandKey, command => { handler((TCommand)command); return null; });
        }
    }
}
