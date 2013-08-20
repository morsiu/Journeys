namespace Journeys.Transactions
{
    /// <summary>
    /// Provides functionality to commit or abort changes done to object in transaction.
    /// </summary>
    public interface ITransactional
    {
        /// <summary>
        /// Discards changes done since last Abort or Commit call.
        /// </summary>
        void Abort();

        /// <summary>
        /// Pushes changes since last Abort or Commit call into underlying object.
        /// </summary>
        void Commit();
    }
}
