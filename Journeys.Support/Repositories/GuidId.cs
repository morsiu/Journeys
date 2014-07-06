using System;
using System.Runtime.Serialization;

namespace Journeys.Support.Repositories
{
    [DataContract]
    public sealed class GuidId
    {
        [DataMember]
        private Guid _id;

        public GuidId(Guid id)
        {
            _id = id;
        }
        
        public override bool Equals(object obj)
        {
            return obj is GuidId && Equals((GuidId)obj);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        private bool Equals(GuidId other)
        {
            return _id == other._id;
        }
    }
}
