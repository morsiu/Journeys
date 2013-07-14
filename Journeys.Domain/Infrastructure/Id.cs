using Journeys.Domain.Infrastructure.Markers;
using System;

namespace Journeys.Domain.Infrastructure
{
    [ValueObject]
    public struct Id<TEntity>
    {
        private Guid _id;

        public Id(Guid id)
        {
            _id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Id<TEntity> == false) return false;
            return Equals((Id<TEntity>)obj);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public bool Equals(Id<TEntity> other)
        {
            return _id == other._id;
        }

        public static bool operator==(Id<TEntity> a, Id<TEntity> b)
        {
            return a.Equals(b);
        }

        public static bool operator!=(Id<TEntity> a, Id<TEntity> b)
        {
            return !a.Equals(b);
        }
    }
}
