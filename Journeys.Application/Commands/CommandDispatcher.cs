namespace Journeys.Application.Commands
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly CommandProcessor _commandProcessor;

        internal CommandDispatcher(CommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public void Dispatch<TCommand>(TCommand command)
        {
            _commandProcessor.Handle(command);
        }
    }
}
