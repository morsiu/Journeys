using System;
using Journeys.Application.EventSourcing;
using Implementation = Mors.Support.EventSourcing;

namespace Journeys.Hosting.Adapters.Modules.EventSourcing
{
    public class EventSourcing : IEventSourcing
    {
        private readonly Implementation.Module _eventSourcingModule;

        public EventSourcing(Implementation.Module eventSourcingModule)
        {
            _eventSourcingModule = eventSourcingModule;
        }

        public void RegisterEventReplayer<TEvent>(Action<TEvent> eventReplayer)
        {
            _eventSourcingModule.Register(eventReplayer);
        }
    }
}
