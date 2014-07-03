using System;

namespace Journeys.Application.Adapters
{
    internal class CommandKey
    {
        private readonly Type _commandType;

        private CommandKey(Type commandType)
        {
            _commandType = commandType;
        }

        public static CommandKey From(object command)
        {
            var commandType = command.GetType();
            return new CommandKey(commandType);
        }

        public static CommandKey From<TCommand>()
        {
            var commandType = typeof(TCommand);
            return new CommandKey(commandType);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(obj, null)
                && obj is CommandKey
                && Equals((CommandKey)obj);
        }

        public override int GetHashCode()
        {
            return _commandType.GetHashCode();
        }

        public override string ToString()
        {
            return _commandType.ToString();
        }

        private bool Equals(CommandKey other)
        {
            return other._commandType.Equals(_commandType);
        }
    }
}
