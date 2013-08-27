namespace Journeys.Command
{
    public interface ICommandHandlerRegistry
    {
        void SetHandler<TCommand>(CommandHandler<TCommand> handler);
    }
}
