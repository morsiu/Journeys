using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Application.Infrastructure.Events
{
    internal delegate void EventListener<TEvent>(TEvent @event);
}
