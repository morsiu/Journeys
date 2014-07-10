using Journeys.Application.EventSourcing;
using Journeys.Support.Repositories;

namespace Journeys.Hosting.Adapters.Modules.EventSourcing
{
    public sealed class EventSourcingIdFactory : IIdFactory
    {
        private readonly GuidIdFactory _idFactory;

        public EventSourcingIdFactory(GuidIdFactory idFactory)
        {
            _idFactory = idFactory;
        }

        public object Create()
        {
            return _idFactory.Create();
        }
    }
}
