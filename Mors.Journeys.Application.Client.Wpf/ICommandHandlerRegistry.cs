using System;

namespace Mors.Journeys.Application.Client.Wpf
{
    public interface ICommandHandlerRegistry
    {
        void SetHandler<TCommand>(Action<TCommand> handler);
    }
}
