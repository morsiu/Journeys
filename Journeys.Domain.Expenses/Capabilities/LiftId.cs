using Journeys.Common;

namespace Journeys.Domain.Expenses.Capabilities
{
    public struct LiftId : IId
    {
        private readonly IId _journeyId;
        private readonly IId _personId;

        public LiftId(IId journeyId, IId personId)
        {
            _journeyId = journeyId;
            _personId = personId;
        }

        public bool Equals(IId other)
        {
            return other is LiftId && Equals((LiftId)other);
        }

        private bool Equals(LiftId other)
        {
            return Equals(_personId, other._personId)
                && Equals(_journeyId, other._journeyId);
        }
    }
}
