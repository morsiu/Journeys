using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journeys.Common;

namespace Journeys.Domain.Test.Infrastructure
{
    internal struct Id : IId
    {
        private readonly int _id;

        public Id(int id)
        {
            _id = id;
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

        public static bool operator ==(Id a, Id b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Id a, Id b)
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
