namespace Journeys.Transactions
{
    public interface ITransacted<T>
    {
        T Object { get; }

        void Abort();

        void Commit();
    }
}
