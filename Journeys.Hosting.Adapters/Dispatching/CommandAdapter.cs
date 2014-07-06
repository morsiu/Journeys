using System;
using Journeys.Hosting.Adapters.Messages;
using Journeys.Support.Dispatching;
using Journeys.Support.Dispatching.Exceptions;

namespace Journeys.Hosting.Adapters.Dispatching
{
    public class CommandAdapter
    {
        private object _command;

        public CommandAdapter(object command)
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
