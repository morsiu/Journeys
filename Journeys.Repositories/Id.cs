using System;
using System.Runtime.Serialization;
using Journeys.Common;

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

        public bool Equals(IId other)
        {
            return other != null
                && other is Id
                && Equals((Id)other);
        }

        private bool Equals(Id other)
        {
            return _id == other._id;
        }
    }
}
