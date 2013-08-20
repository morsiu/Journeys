namespace Journeys.Transactions
{
    /// <summary>
    /// Specifies that this object can provide transactional version of itself.
    /// </summary>
    /// <typeparam name="T">The type of object.</typeparam>
    public interface IProvideTransactional<T>
    {
        /// <summary>
        /// Returns wrapper for self that provides identical functionality with transactional behavior.
        /// </summary>
        ITransactional<T> Lift();
    }
}
