using Journeys.Domain.Markers;

namespace Journeys.Domain.Identities
{
    [ValueObject]
    public struct Id<T>
    {
        private int _id;

        public Id(int id)
        {
            _id = id;
        }
    }
}
