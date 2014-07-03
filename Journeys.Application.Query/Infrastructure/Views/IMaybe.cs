namespace Journeys.Application.Query.Infrastructure.Views
{
    internal interface IMaybe<out T>
    {
        T Value { get; }

        bool HasValue { get; }
    }
}