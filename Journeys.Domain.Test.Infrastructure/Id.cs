using Journeys.Common;

namespace Journeys.Domain.Test.Infrastructure
{
    public struct Id : IId
    {
        private readonly int _id;

        public Id(int id)
        {
            _id = id;
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj is Id
                && Equals((Id)obj);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        private bool Equals(Id other)
        {
            return _id == other._id;
        }

        public bool Equals(IId other)
        {
            return other != null
                && other is Id
                && Equals((Id)other);
        }
    }
}
