using System.Runtime.Serialization;

namespace Journeys.Queries.Dtos
{
    [DataContract]
    public class PassengerLiftsCost
    {
        public PassengerLiftsCost(decimal totalConst)
        {
            TotalCost = totalConst;
        }

        [DataMember]
        public decimal TotalCost { get; private set; }
    }
}
