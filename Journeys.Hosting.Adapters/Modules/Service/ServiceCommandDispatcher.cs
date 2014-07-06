using Journeys.Hosting.Adapters.Dispatching;
using Journeys.Support.Dispatching;

namespace Journeys.Hosting.Adapters.Modules.Service
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
            var commandAdapter = new CommandAdapter(command);
            commandAdapter.Execute(_handlerDispatcher);
        }
    }
}
