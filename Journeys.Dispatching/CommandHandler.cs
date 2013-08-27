namespace Journeys.Dispatching
{
    public delegate void CommandHandler<TCommand>(TCommand command);
}
