using Journeys.Application;
using Mors.Support.Repositories;

namespace Journeys.Adapters
{
    public class ApplicationIdFactory : IIdFactory
    {
        private readonly GuidIdFactory _idFactory;

        public ApplicationIdFactory(GuidIdFactory idFactory)
        {
            _idFactory = idFactory;
        }

        public object Create()
        {
            return _idFactory.Create();
        }
    }
}
