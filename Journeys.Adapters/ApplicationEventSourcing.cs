using System;
using Journeys.Application;
using Implementation = Mors.Support.EventSourcing;

namespace Journeys.Adapters
{
    public class ApplicationEventSourcing : IEventSourcing
    {
        private readonly Implementation.Module _eventSourcingModule;

        public ApplicationEventSourcing(Implementation.Module eventSourcingModule)
        {
            _eventSourcingModule = eventSourcingModule;
        }

        public void RegisterEventReplayer<TEvent>(Action<TEvent> eventReplayer)
        {
            _eventSourcingModule.Register<TEvent>(eventReplayer);            
        }
    }
}
