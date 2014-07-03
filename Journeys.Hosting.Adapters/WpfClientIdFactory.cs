using Mors.Support.Repositories;

namespace Journeys.Application.Adapters
{
    public class WpfClientIdFactory : Client.Wpf.IIdFactory
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
