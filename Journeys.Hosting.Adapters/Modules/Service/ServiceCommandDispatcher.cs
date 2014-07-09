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

        public void Dispatch<TCommand>(TCommand commandSpecification)
        {
            var command = new Dispatching.Command(commandSpecification);
            command.Execute(_handlerDispatcher);
        }
    }
}
