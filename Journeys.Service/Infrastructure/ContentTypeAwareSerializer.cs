using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;

namespace Journeys.Service.Infrastructure
{
    internal sealed class ContentTypeAwareSerializer
    {
        private readonly NetDataContractSerializer _xmlSerializer = new NetDataContractSerializer();

        public object Deserialize(Stream contentStream, string contentType)
        {
            switch (MatchContentType(contentType))
            {
                case ContentType.Json:
                    return DeserializeJson(contentStream);
                case ContentType.Xml:
                    return _xmlSerializer.Deserialize(contentStream);
                default:
                    throw new ArgumentException("Unsupported content type: {0}", contentType);
            }
        }

        public string Serialize(object content, Stream contentStream, IEnumerable<Tuple<string, decimal>> contentTypes)
        {
            var selectedContentType = SelectContentType(contentTypes);
            switch (MatchContentType(selectedContentType))
            {
                case ContentType.Json:
                    SerializeJson(content, contentStream);
                    break;
                case ContentType.Xml:
                    _xmlSerializer.Serialize(contentStream, content);
                    break;
                default:
                    throw new ArgumentException("Unsupported content types: {0}", string.Join(", ", contentTypes.Select(ct => ct.Item1)));
            }
            return selectedContentType;
        }

        private static void SerializeJson(object content, Stream contentStream)
        {
            var serializedContent = JsonConvert.SerializeObject(content);
            var writer = new StreamWriter(contentStream);
            writer.Write(serializedContent);
            writer.Flush();
        }

        private static object DeserializeJson(Stream contentStream)
        {
            var reader = new StreamReader(contentStream);
            var content = reader.ReadToEnd();
            return JsonConvert.DeserializeObject(content, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
        }

        private static string SelectContentType(IEnumerable<Tuple<string,decimal>> contentTypes)
        {
            return contentTypes.Select(ct => ct.Item1)
                .Where(ct => MatchContentType(ct) != ContentType.Unknown)
                .FirstOrDefault();
        }

        private static ContentType MatchContentType(string contentType)
        {
            if (contentType.Contains("xml"))
            {
                return ContentType.Xml;
            }
            else if (contentType.Contains("json"))
            {
                return ContentType.Json;
            }
            return ContentType.Unknown;
        }

        private enum ContentType
        {
            Unknown,
            Xml,
            Json,
        }
    }
}
