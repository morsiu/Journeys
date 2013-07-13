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

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Id<T> == false) return false;
            return Equals((Id<T>)obj);
        }

        public override int GetHashCode()
        {
            return _id;
        }

        public bool Equals(Id<T> other)
        {
            return _id == other._id;
        }

        public static bool operator==(Id<T> a, Id<T> b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(Id<T> a, Id<T> b)
        {
            return !a.Equals(b);
        }
    }
}
