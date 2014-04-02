using Journeys.Adapters;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;

namespace Journeys.Service.Controllers
{
    public class CommandController : ApiController
    {
        public static ServiceCommandDispatcher CommandDispatcher;

        [Route("api/command")]
        public async Task Post()
        {
            var serializedCommand = await Request.Content.ReadAsStreamAsync();
            var dataSerializer = new NetDataContractSerializer();
            var command = dataSerializer.Deserialize(serializedCommand);
            CommandDispatcher.Dispatch(command);
        }
    }
}
