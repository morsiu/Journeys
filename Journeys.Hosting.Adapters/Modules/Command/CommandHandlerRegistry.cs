using System;
using Journeys.Application.Command;
using Journeys.Hosting.Adapters.Dispatching;
using Mors.Support.Dispatching;

namespace Journeys.Hosting.Adapters.Modules.Command
{
    public class CommandHandlerRegistry : ICommandHandlerRegistry
    {
        private readonly HandlerRegistry _handlerRegistry;

        public CommandHandlerRegistry(HandlerRegistry handlerRegistry)
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
