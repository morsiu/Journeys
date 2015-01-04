namespace Mors.Journeys.Application
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command);
    }
}
