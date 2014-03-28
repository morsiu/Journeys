using Journeys.Adapters;
using Journeys.Commands;
using Journeys.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;

namespace Journeys.Service.Controllers
{
    public class CommandController : ApiController
    {
        private static readonly IReadOnlyCollection<Type> SupportedQueryTypes;

        static CommandController()
        {
            var idFactory = new IdFactory();
            SupportedQueryTypes = Assembly.GetAssembly(typeof(AddJourneyWithLiftsCommand)).GetExportedTypes().Where(t => t.IsClass).Concat(new[] { idFactory.IdImplementationType }).ToList();
        }

        public static ServiceCommandDispatcher CommandDispatcher;

        [Route("api/command")]
        public async Task Post()
        {
            
            var serializedQuery = await Request.Content.ReadAsStreamAsync();
            var dataSerializer = new DataContractSerializer(typeof(object), SupportedQueryTypes);
            var responseStream = new MemoryStream();
            var query = dataSerializer.ReadObject(serializedQuery);
            CommandDispatcher.Dispatch(query);
        }
    }
}
