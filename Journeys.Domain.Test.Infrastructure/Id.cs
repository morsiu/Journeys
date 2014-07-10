namespace Journeys.Domain.Test.Infrastructure
{
    public class Id
    {
        private readonly int _id;

        public Id(int id)
        {
            _id = id;
        }

        public override bool Equals(object other)
        {
            return other != null
                && other is Id
                && Equals((Id)other);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        private bool Equals(Id other)
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
    }
}
