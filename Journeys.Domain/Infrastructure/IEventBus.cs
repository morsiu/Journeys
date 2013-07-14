using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain.Infrastructure
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event);
    }
}
