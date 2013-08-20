namespace Journeys.Transactions
{
    public interface ITransacted<T> : ITransactable
    {
        T Object { get; }
    }
}
