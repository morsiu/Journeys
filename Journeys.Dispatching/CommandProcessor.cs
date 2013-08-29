using System;
using System.Collections.Generic;
using Journeys.Dispatching.Exceptions;
using Journeys.Dispatching.Messages;

namespace Journeys.Dispatching
{
    public class CommandProcessor
    {
        private readonly HandlerDispatcher _dispatcher;
        private readonly HandlerRegistry _handlers;

        public CommandProcessor()
        {
            _handlers = new HandlerRegistry();
            _dispatcher = new HandlerDispatcher(_handlers);
        }

        public void SetHandler<TCommand>(CommandHandler<TCommand> handler)
        {
            var commandType = typeof(TCommand);
            _handlers.Set(commandType, command => { handler((TCommand)command); return null; });
        }
        
        public void Handle(object command)
        {
            var commandType = command.GetType();
            try
            {
                _dispatcher.Dispatch(commandType, command);
            }
            catch (HandlerNotFoundException)
            {
                throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForCommandOfType, commandType));
            }
        }
    }
}
