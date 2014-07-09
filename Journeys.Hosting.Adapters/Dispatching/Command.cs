using System;
using Journeys.Hosting.Adapters.Messages;
using Journeys.Support.Dispatching;
using Journeys.Support.Dispatching.Exceptions;

namespace Journeys.Hosting.Adapters.Dispatching
{
    public class Command
    {
        private object _commandSpecification;

        public Command(object commandSpecification)
        {
            _commandSpecification = commandSpecification;
        }

        public void Execute(HandlerDispatcher dispatcher)
        {
            var commandKey = CommandKey.From(_commandSpecification);
            try
            {
                dispatcher.Dispatch(commandKey, _commandSpecification);
            }
            catch (HandlerNotFoundException)
            {
                throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForCommandOfType, commandKey));
            }
        }
    }
}
