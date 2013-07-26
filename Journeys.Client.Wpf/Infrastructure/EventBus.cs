using Journeys.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Client.Wpf.Infrastructure
{
    internal class EventBus
    {
        private Eventing.EventBus _eventBus = new Eventing.EventBus();

        public void Publish<TEvent>(TEvent @event)
        {
            _eventBus.Publish(@event);
        }

        public void Subscribe<TEvent>(EventListener<TEvent> listener)
        {
            _eventBus.RegisterListener(listener);
        }
    }
}
