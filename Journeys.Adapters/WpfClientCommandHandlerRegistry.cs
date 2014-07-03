using System;
using Journeys.Client.Wpf;
using Mors.Support.Dispatching;

namespace Journeys.Adapters
{
    public class WpfClientCommandHandlerRegistry : ICommandHandlerRegistry
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
