using Journeys.Domain.Infrastructure.Markers;

namespace Journeys.Domain.Expenses.Capabilities
{
    [ValueObject]
    internal struct Money
    {
        private readonly decimal _amount;

        public Money(decimal amount)
            : this()
        {
            _amount = amount;
        }

        public decimal Amount { get { return _amount; } }

        public static Money operator+(Money a, Money b)
        {
            return new Money(a.Amount + b.Amount);
        }

        public static Money operator *(Money a, decimal b)
        {
            return new Money(a.Amount * b);
        }

        public static Money operator *(decimal a, Money b)
        {
            return new Money(a * b.Amount);
        }

        public static Money operator /(Money a, decimal b)
        {
            return new Money(a.Amount / b);
        }
    }
}
