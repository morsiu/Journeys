using System;

namespace Journeys.Support.Repositories
{
    public sealed class GuidIdFactory
    {
        public Guid Create()
        {
            return Guid.NewGuid();
        }

        public Type IdImplementationType
        {
            get { return typeof(Guid); }
        }
    }
}
