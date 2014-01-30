using System;
using Journeys.Dispatching;
using Journeys.Client.Wpf;

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
