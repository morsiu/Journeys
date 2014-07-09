using System;
using System.Threading.Tasks;
using Journeys.Hosting.Adapters.Messages;
using Journeys.Support.Dispatching;
using Journeys.Support.Dispatching.Exceptions;

namespace Journeys.Hosting.Adapters.Dispatching
{
    public sealed class AsyncCommand
    {
        private object _commandSpecification;

        public AsyncCommand(object commandSpecification)
        {
            _commandSpecification = commandSpecification;
        }

        public Task Execute(AsyncHandlerDispatcher dispatcher)
        {
            var commandKey = CommandKey.From(_commandSpecification);
            try
            {
                return dispatcher.Dispatch(commandKey, _commandSpecification);
            }
            catch (HandlerNotFoundException)
            {
                throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForCommandOfType, commandKey));
            }
        }
    }
}
