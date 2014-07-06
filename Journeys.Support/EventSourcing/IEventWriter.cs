namespace Journeys.Support.EventSourcing
{
    internal interface IEventWriter
    {
        void Write<TEvent>(TEvent @event);
    }
}
