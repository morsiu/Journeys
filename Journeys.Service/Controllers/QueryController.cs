using Journeys.Adapters;
using Journeys.Application;
using Journeys.Queries;
using Journeys.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Journeys.Service.Controllers
{
    public class QueryController : ApiController
    {
        private static readonly IReadOnlyCollection<Type> SupportedQueryTypes;

        static QueryController()
        {
            var idFactory = new IdFactory();
            SupportedQueryTypes = GetSupportedQueryTypes(Assembly.GetAssembly(typeof(IQuery<>)).GetExportedTypes().Where(t => t.IsClass).Concat(new[] { idFactory.IdImplementationType })).ToList();
        }

        private static IEnumerable<Type> GetSupportedQueryTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                if (type.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IQuery<>)))
                {
                    yield return type;
                }
                else
                {
                    yield return type;
                    yield return typeof(List<>).MakeGenericType(type);
                }
            }
        }

        public static ServiceQueryDispatcher QueryDispatcher;

        [Route("api/query")]
        public async Task<HttpResponseMessage> Post()
        {
            
            var serializedQuery = await Request.Content.ReadAsStreamAsync();
            var dataSerializer = new DataContractSerializer(typeof(object), SupportedQueryTypes);
            var responseStream = new MemoryStream();
            var query = dataSerializer.ReadObject(serializedQuery);
            var result = QueryDispatcher.Dispatch(query);
            try
            {
                dataSerializer.WriteObject(responseStream, result);
                responseStream.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(responseStream);
                var responseText = reader.ReadToEnd();
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(responseText, Encoding.UTF8, "text/plain");
                return response;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
