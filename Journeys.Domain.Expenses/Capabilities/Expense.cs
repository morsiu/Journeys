using Journeys.Common;
using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Capabilities
{
    [ValueObject]
    public struct Expense
    {
        private readonly IId _subjectId;
        private readonly Money _value;

        public Expense(IId subjectId, Money value)
        {
            _subjectId = subjectId;
            _value = value;
        }

        public IId SubjectId { get { return _subjectId; } }

        public Money Value { get { return _value; } }

        public Expense Increase(Money value)
        {
            return new Expense(_subjectId, _value + value);
        }
    }
}
