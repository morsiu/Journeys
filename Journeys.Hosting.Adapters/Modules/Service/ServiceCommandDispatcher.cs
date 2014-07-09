using System.Threading.Tasks;
using Journeys.Support.Dispatching;

namespace Journeys.Hosting.Adapters.Modules.Service
{
    public class ServiceCommandDispatcher
    {
        private readonly AsyncHandlerDispatcher _handlerDispatcher;

        public ServiceCommandDispatcher(AsyncHandlerDispatcher handlerDispatcher)
        {
            _handlerDispatcher = handlerDispatcher;
        }

        public Task Dispatch<TCommand>(TCommand commandSpecification)
        {
            var command = new Dispatching.AsyncCommand(commandSpecification);
            return command.Execute(_handlerDispatcher);
        }
    }
}
