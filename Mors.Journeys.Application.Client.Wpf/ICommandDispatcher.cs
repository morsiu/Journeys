namespace Mors.Journeys.Application.Client.Wpf
{
    public interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command);
    }
}
