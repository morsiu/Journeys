using Journeys.Command;
using Journeys.Dispatching;

namespace Journeys.Adapters
{
    public class CommandHandlerRegistry : ICommandHandlerRegistry
    {
        private readonly HandlerRegistry _handlerRegistry;

        public CommandHandlerRegistry(HandlerRegistry handlerRegistry)
        {
            _handlerRegistry = handlerRegistry;
        }

        public void SetHandler<TCommand>(Journeys.Command.CommandHandler<TCommand> handler)
        {
            var commandKey = CommandKey.From<TCommand>();
            _handlerRegistry.Set(commandKey, command => { handler((TCommand)command); return null; });
        }
    }
}
