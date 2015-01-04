using Mors.Journeys.Application;
using Mors.Journeys.Data.Events;
using Mors.Journeys.Domain.Journeys.Capabilities;
using Mors.Journeys.Domain.Journeys.Operations;

namespace Mors.Journeys.Application.EventReplayers
{
    internal sealed class LiftAddedEventReplayer
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
