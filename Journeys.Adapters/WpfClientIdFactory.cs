using Journeys.Client.Wpf;
using Journeys.Common;
using Journeys.Repositories;

namespace Journeys.Adapters
{
    public class WpfClientIdFactory : IIdFactory
    {
        private readonly IdFactory _idFactory;

        public WpfClientIdFactory(IdFactory idFactory)
        {
            _idFactory = idFactory;
        }

        public IId Create()
        {
            return _idFactory.Create();
        }
    }
}
