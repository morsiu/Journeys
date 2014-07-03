namespace Journeys.Domain.Expenses.Capabilities
{
    public struct LiftId
    {
        private readonly object _journeyId;
        private readonly object _personId;

        public LiftId(object journeyId, object personId)
        {
            _journeyId = journeyId;
            _personId = personId;
        }

        public override bool Equals(object other)
        {
            return other is LiftId && Equals((LiftId)other);
        }

        public override int GetHashCode()
        {
            return unchecked(_journeyId.GetHashCode() * 397 ^ _personId.GetHashCode());
        }

        private bool Equals(LiftId other)
        {
            return Equals(_personId, other._personId)
                && Equals(_journeyId, other._journeyId);
        }

        public static bool operator ==(LiftId a, LiftId b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(LiftId a, LiftId b)
        {
            return !a.Equals(b);
        }
    }
}
