using System;
using System.Runtime.Serialization;
using Journeys.Common;
using Journeys.Repositories;

namespace Journeys.Client.Wpf.Infrastructure
{
    [DataContract]
    internal struct Id : IId
    {
        [DataMember]
        private Guid _id;

        public Id(Guid id)
        {
            _id = id;
        }

        public static IId New()
        {
            return new Id(Guid.NewGuid());
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

        public bool Equals(IId other)
        {
            return other is Id
                && Equals((Id)other);
        }
    }
}
