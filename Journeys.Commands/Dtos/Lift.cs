using System.Runtime.Serialization;

namespace Journeys.Commands.Dtos
{
    [DataContract]
    public class Lift
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
