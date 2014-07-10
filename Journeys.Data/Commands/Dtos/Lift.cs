using System.Runtime.Serialization;

namespace Journeys.Data.Commands.Dtos
{
    [DataContract]
    public sealed class Lift
    {
        public Lift(string personName, decimal liftDistance)
        {
            PersonName = personName;
            LiftDistance = liftDistance;
        }

        [DataMember]
        public decimal LiftDistance { get; private set; }

        [DataMember]
        public string PersonName { get; private set; }
    }
}
