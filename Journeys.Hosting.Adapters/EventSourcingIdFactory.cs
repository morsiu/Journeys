using Journeys.Application.EventSourcing;
using Mors.Support.Repositories;

namespace Journeys.Application.Adapters
{
    public class EventSourcingIdFactory : IIdFactory
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
