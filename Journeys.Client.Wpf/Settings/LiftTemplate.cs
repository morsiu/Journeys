namespace Journeys.Client.Wpf.Settings
{
    internal sealed class LiftTemplate
    {
        public string PassengerName { get; set; }

        public decimal Distance { get; set; }

        public LiftTemplate Clone()
        {
            return new LiftTemplate
            {
                PassengerName = PassengerName,
                Distance = Distance
            };
        }
    }
}
