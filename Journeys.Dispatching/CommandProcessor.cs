using System;
using System.Collections.Generic;
using Journeys.Dispatching.Messages;

namespace Journeys.Dispatching
{
    public class CommandProcessor
    {
        private delegate void UntypedCommandHandler(object command);
        private readonly HandlerRegistry _handlers = new HandlerRegistry();

        public void SetHandler<TCommand>(CommandHandler<TCommand> handler)
        {
            var commandType = typeof(TCommand);
            _handlers.Set(commandType, command => { handler((TCommand)command); return null; });
        }
        
        public void Handle(object command)
        {
            var commandType = command.GetType();
            Func<object, object> handler;
            if (_handlers.Retrieve(commandType, out handler))
            {
                handler(command);
            }
            else
            {
                throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForCommandOfType, commandType));
            }
        }
    }
}
