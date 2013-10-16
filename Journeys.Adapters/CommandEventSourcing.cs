using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journeys.Command;

namespace Journeys.Adapters
{
    public class CommandEventSourcing : IEventSourcing
    {
        private readonly EventSourcing.Module _eventSourcingModule;

        public CommandEventSourcing(EventSourcing.Module eventSourcingModule)
        {
            _eventSourcingModule = eventSourcingModule;
        }

        public void RegisterEventReplayer<TEvent>(Action<TEvent> replayer)
        {
            _eventSourcingModule.Register<TEvent>(replayer);            
        }
    }
}
