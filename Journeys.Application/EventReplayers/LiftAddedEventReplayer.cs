using Journeys.Domain.Journeys.Capabilities;
using Journeys.Domain.Journeys.Operations;
using Journeys.Data.Events;

namespace Journeys.Application.EventReplayers
{
    internal class LiftAddedEventReplayer
    {
        private readonly IRepositories _repositories;

        public LiftAddedEventReplayer(IRepositories repositories)
        {
            _repositories = repositories;
        }

        public void Replay(LiftAddedEvent @event)
        {
            var liftDistance = new Distance(@event.LiftDistance, DistanceUnit.Kilometer);
            var journey = _repositories
                .Get<Journey>(@event.JourneyId)
                .AddLift(@event.PersonId, liftDistance);
            _repositories.Store(journey);
        }
    }
}
