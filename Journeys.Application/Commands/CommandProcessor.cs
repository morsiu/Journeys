using Journeys.Application.Messages;
using System;
using System.Collections.Generic;

namespace Journeys.Application.Commands
{
    internal class CommandProcessor
    {
        private readonly Dictionary<Type, Action<object>> _handlers = new Dictionary<Type, Action<object>>();

        public void SetHandler<TCommand>(CommandHandler<TCommand> handler)
        {
            var commandType = typeof(TCommand);
            Action<object> untypedHandler = command => handler((TCommand)command);
            _handlers[commandType] = untypedHandler;
        }

        public void Handle(object command)
        {
            if (command == null) throw new ArgumentNullException("command");
            var untypedHandler = GetHandler(command);
            untypedHandler(command);
        }

        private Action<object> GetHandler(object command)
        {
            var commandType = command.GetType();
            if (!_handlers.ContainsKey(commandType)) throw new InvalidOperationException(string.Format(FailureMessages.NoHandlerRegisteredForCommandOfType, commandType));
            var untypedHandler = _handlers[commandType];
            return untypedHandler;
        }
    }
}
