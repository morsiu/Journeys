using System;

namespace Journeys.Application.Command
{
    public interface ICommandHandlerRegistry
    {
        void SetHandler<TCommand>(Action<TCommand> handler);
    }
}
