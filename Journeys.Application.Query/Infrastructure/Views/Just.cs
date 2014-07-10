namespace Journeys.Application.Query.Infrastructure.Views
{
    internal sealed class Just<T> : IMaybe<T>
    {
        public Just(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }

        public bool HasValue
        {
            get { return true; }
        }
    }
}