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
        private readonly EventSourcing.Bootstrapper _eventSourcingBootstrapper;

        public CommandEventSourcing(EventSourcing.Bootstrapper eventSourcingBootstrapper)
        {
            _eventSourcingBootstrapper = eventSourcingBootstrapper;
        }

        public void RegisterEventReplayer<TEvent>(Action<TEvent> replayer)
        {
            _eventSourcingBootstrapper.Register<TEvent>(replayer);            
        }
    }
}
