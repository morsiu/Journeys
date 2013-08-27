using Journeys.Dispatching;

namespace Journeys.Client.Wpf.Infrastructure
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly CommandProcessor _commandProcessor;

        public CommandDispatcher(CommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public void Dispatch<TCommand>(TCommand command)
        {
            _commandProcessor.Handle(command);
        }
    }
}
