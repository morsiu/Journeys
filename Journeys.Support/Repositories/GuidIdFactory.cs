using System;

namespace Journeys.Support.Repositories
{
    public sealed class GuidIdFactory
    {
        public GuidId Create()
        {
            return new GuidId(Guid.NewGuid());
        }

        public Type IdImplementationType
        {
            get { return typeof(GuidId); }
        }
    }
}
