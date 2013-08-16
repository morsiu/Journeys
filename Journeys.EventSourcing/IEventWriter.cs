namespace Journeys.EventSourcing
{
    internal interface IEventWriter
    {
        void Write<TEvent>(TEvent @event);
    }
}
