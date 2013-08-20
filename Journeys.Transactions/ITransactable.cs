namespace Journeys.Transactions
{
    public interface ITransactable
    {
        void Abort();

        void Commit();
    }
}
