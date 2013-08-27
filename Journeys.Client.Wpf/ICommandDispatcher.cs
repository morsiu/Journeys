namespace Journeys.Client.Wpf
{
    internal interface ICommandDispatcher
    {
        void Dispatch<TCommand>(TCommand command);
    }
}
