using Journeys.Dispatching;

namespace Journeys.Adapters
{
    public class ServiceCommandDispatcher
    {
        private readonly HandlerDispatcher _handlerDispatcher;

        public ServiceCommandDispatcher(HandlerDispatcher handlerDispatcher)
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
