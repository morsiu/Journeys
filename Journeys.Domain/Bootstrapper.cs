using Journeys.Domain.Infrastructure.Repositories;

namespace Journeys.Domain
{
    public class Bootstrapper
    {
        public IDomainRepositories DomainRepositories { get; private set; }

        public void Bootstrap()
        {
            DomainRepositories = new DomainRepositories();
        }
    }
}
