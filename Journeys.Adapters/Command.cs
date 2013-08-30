using System;
using Journeys.Adapters.Messages;
using Journeys.Dispatching;
using Journeys.Dispatching.Exceptions;

namespace Journeys.Adapters
{
    public class Command
    {
        private object _command;

        public Command(object command)
        {
            _command = command;
        }

        public void Execute(HandlerDispatcher dispatcher)
        {
            var commandKey = CommandKey.From(_command);
            try
            {
                dispatcher.Dispatch(commandKey, _command);
            }
            catch (HandlerNotFoundException)
            {
                throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForCommandOfType, commandKey));
            }
        }
    }
}
