namespace Journeys.Application.Commands
{
    internal delegate void CommandHandler<TCommand>(TCommand command);
}
