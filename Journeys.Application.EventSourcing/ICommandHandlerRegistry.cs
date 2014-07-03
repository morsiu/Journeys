using System;

namespace Journeys.Application.EventSourcing
{
    public interface ICommandHandlerRegistry
    {
        void SetHandler<TCommand>(Action<TCommand> handler);
    }
}
