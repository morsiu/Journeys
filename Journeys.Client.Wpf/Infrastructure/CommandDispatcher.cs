using Journeys.Dispatching;

namespace Journeys.Client.Wpf.Infrastructure
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly HandlerDispatcher _handlerDispatcher;

        public CommandDispatcher(HandlerDispatcher handlerDispatcher)
        {
            _handlerDispatcher = handlerDispatcher;
        }

        public void Dispatch<TCommand>(TCommand command)
        {
            var commandAdapter = new Adapters.Command(command);
            commandAdapter.Execute(_handlerDispatcher);
        }
    }
}
