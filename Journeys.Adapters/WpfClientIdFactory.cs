using Journeys.Client.Wpf;
using Mors.Support.Repositories;

namespace Journeys.Adapters
{
    public class WpfClientIdFactory : IIdFactory
    {
        private readonly GuidIdFactory _idFactory;

        public WpfClientIdFactory(GuidIdFactory idFactory)
        {
            _idFactory = idFactory;
        }

        public object Create()
        {
            return _idFactory.Create();
        }
    }
}
