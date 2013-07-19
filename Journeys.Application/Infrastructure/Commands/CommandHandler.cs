namespace Journeys.Application.Infrastructure.Commands
{
    internal delegate void CommandHandler<TCommand>(TCommand command);
}
