using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journeys.Application;

namespace Journeys.Adapters
{
    public class ApplicationEventSourcing : IEventSourcing
    {
        private readonly EventSourcing.Module _eventSourcingModule;

        public ApplicationEventSourcing(EventSourcing.Module eventSourcingModule)
        {
            _eventSourcingModule = eventSourcingModule;
        }

        public void RegisterEventReplayer<TEvent>(Action<TEvent> eventReplayer)
        {
            _eventSourcingModule.Register<TEvent>(eventReplayer);            
        }
    }
}
