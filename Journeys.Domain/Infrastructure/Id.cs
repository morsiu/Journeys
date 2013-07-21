using Journeys.Domain.Infrastructure.Markers;
using System;

namespace Journeys.Domain.Infrastructure
{
    [ValueObject]
    public struct Id
    {
        private Guid _id;

        public Id(Guid id)
        {
            _id = id;
        }

        public static implicit operator Guid(Id id)
        {
            return id._id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Id == false) return false;
            return Equals((Id)obj);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public bool Equals(Id other)
        {
            return _id == other._id;
        }

        public static bool operator==(Id a, Id b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(Id a, Id b)
        {
            return !a.Equals(b);
        }
    }
}
