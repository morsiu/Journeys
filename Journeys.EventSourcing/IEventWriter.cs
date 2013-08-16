using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.EventSourcing
{
    public interface IEventWriter
    {
        void Write<TEvent>(TEvent @event);
    }
}
