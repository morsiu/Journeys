using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Command
{
    public interface IEventSourcing
    {
        void RegisterEventReplayer<TEvent>(Action<TEvent> replayer);
    }
}
