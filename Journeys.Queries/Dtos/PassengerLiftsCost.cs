namespace Journeys.Queries.Dtos
{
    public class PassengerLiftsCost
    {
        public PassengerLiftsCost(decimal totalConst)
        {
            TotalCost = totalConst;
        }

        public decimal TotalCost { get; private set; }
    }
}
