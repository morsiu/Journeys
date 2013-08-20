namespace Journeys.Transactions
{
    /// <summary>
    /// Exposes transactional wrapper on object with functionality to control it.
    /// </summary>
    /// <typeparam name="T">Type of underlying object.</typeparam>
    public interface ITransactional<T> : ITransactional
    {
        /// <summary>
        /// Gets the transactional wrapper on object.
        /// </summary>
        T Object { get; }
    }
}
