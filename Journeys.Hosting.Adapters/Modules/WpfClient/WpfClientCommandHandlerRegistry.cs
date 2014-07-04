using System;
using Journeys.Hosting.Adapters.Dispatching;
using Mors.Support.Dispatching;

namespace Journeys.Hosting.Adapters.Modules.WpfClient
{
    public class WpfClientCommandHandlerRegistry : Application.Client.Wpf.ICommandHandlerRegistry
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
