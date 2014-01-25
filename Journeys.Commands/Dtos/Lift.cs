namespace Journeys.Commands.Dtos
{
    public class Lift
    {
        public Lift(string personName, decimal liftDistance)
        {
            PersonName = personName;
            LiftDistance = liftDistance;
        }

        public decimal LiftDistance { get; private set; }

        public string PersonName { get; private set; }
    }
}
