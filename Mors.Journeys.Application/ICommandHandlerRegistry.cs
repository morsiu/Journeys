using System;

namespace Mors.Journeys.Application
{
    public interface ICommandHandlerRegistry
    {
        void SetHandler<TCommand>(Action<TCommand> handler);
    }
}
