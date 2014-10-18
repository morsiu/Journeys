using System.Runtime.Serialization;

namespace Mors.Journeys.Data.Queries.Dtos
{
    [DataContract]
    public sealed class PassengerLiftsCost
    {
        public PassengerLiftsCost(decimal totalConst)
        {
            TotalCost = totalConst;
        }

        [DataMember]
        public decimal TotalCost { get; private set; }
    }
}
