namespace Journeys.Application.Query.Infrastructure.Views
{
    internal static class ObjectExtensions
    {
        public static bool IsNotNull<T>(this T value)
        {
            return !ReferenceEquals(value, null);
        }

        public static bool IsNull<T>(this T value)
        {
            return ReferenceEquals(value, null);
        }
    }
}
