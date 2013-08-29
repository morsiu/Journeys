using System;
using System.Collections.Generic;
using Journeys.Dispatching.Messages;

namespace Journeys.Dispatching
{
    public class CommandProcessor
    {
        private delegate void UntypedCommandHandler(object command);
        private readonly Dictionary<Type, UntypedCommandHandler> _handlers = new Dictionary<Type, UntypedCommandHandler>();

        public void SetHandler<TCommand>(CommandHandler<TCommand> handler)
        {
            var commandType = typeof(TCommand);
            UntypedCommandHandler untypedHandler = command => handler((TCommand)command);
            _handlers[commandType] = untypedHandler;
        }

        public void Handle(object command)
        {
            if (command == null) throw new ArgumentNullException("command");
            var untypedHandler = GetHandler(command);
            untypedHandler(command);
        }

        private UntypedCommandHandler GetHandler(object command)
        {
            var commandType = command.GetType();
            if (!_handlers.ContainsKey(commandType)) throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForCommandOfType, commandType));
            var untypedHandler = _handlers[commandType];
            return untypedHandler;
        }
    }
}
