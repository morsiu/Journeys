namespace Journeys.Command.Infrastructure
{
    internal delegate void CommandHandler<TCommand>(TCommand command);
}
