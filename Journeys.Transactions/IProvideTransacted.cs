namespace Journeys.Transactions
{
    public interface IProvideTransacted<T>
    {
        ITransacted<T> Lift();
    }
}
