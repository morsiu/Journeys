using Journeys.Adapters;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Journeys.Service.Controllers
{
    public class QueryController : ApiController
    {
        public static ServiceQueryDispatcher QueryDispatcher;

        [Route("api/query")]
        public async Task<HttpResponseMessage> Post()
        {
            var serializedQuery = await Request.Content.ReadAsStreamAsync();
            var dataSerializer = new NetDataContractSerializer();
            var query = dataSerializer.Deserialize(serializedQuery);
            var result = QueryDispatcher.Dispatch(query);
            var responseStream = new MemoryStream();
            dataSerializer.Serialize(responseStream, result);
            responseStream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(responseStream);
            var responseText = reader.ReadToEnd();
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(responseText, Encoding.UTF8, "application/xml");
            return response;
        }
    }
}
