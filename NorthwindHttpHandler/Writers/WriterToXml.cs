using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace NorthwindHttpHandler.Writers
{
    public class WriterToXml : IWriter
    {
        public void Write<T>(IEnumerable<T> data, Stream outStream)
        {
            var xmlSerializer = new XmlSerializer(typeof(T[]));

            using (outStream)
            {
                using (var writer = XmlWriter.Create(outStream))
                {
                    xmlSerializer.Serialize(writer, data.ToArray());
                }
            }
        }
    }
}