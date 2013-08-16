using Journeys.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journeys.Domain
{
    public class Bootstrapper
    {
        public DomainRepositories DomainRepositories { get; private set; }

        public void Bootstrap()
        {
            DomainRepositories = new DomainRepositories();
        }
    }
}
