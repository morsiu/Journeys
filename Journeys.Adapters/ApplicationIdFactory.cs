using Journeys.Application;
using Journeys.Common;
using Journeys.Repositories;

namespace Journeys.Adapters
{
    public class ApplicationIdFactory : IIdFactory
    {
        private readonly IdFactory _idFactory;

        public ApplicationIdFactory(IdFactory idFactory)
        {
            _idFactory = idFactory;
        }

        public IId Create()
        {
            return _idFactory.Create();
        }
    }
}
