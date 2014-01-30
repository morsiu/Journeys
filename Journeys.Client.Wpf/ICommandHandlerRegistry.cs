using System;

namespace Journeys.Client.Wpf
{
    public interface ICommandHandlerRegistry
    {
        void SetHandler<TCommand>(Action<TCommand> handler);
    }
}
