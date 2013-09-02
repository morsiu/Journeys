using Journeys.Client.Wpf;
using Journeys.Dispatching;

namespace Journeys.Adapters
{
    public class WpfClientCommandDispatcher : ICommandDispatcher
    {
        private readonly HandlerDispatcher _handlerDispatcher;

        public WpfClientCommandDispatcher(HandlerDispatcher handlerDispatcher)
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
