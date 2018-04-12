using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using NorthwindHttpHandler.Writers;

namespace NorthwindHttpHandler
{
    public static class Extentions
    {
        private const string ContentDisposition = "content-disposition";
        private const string FilePath = "attachment;filename=\"{0}\"";
        private const string DefaultType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


        public static void GetFilterFromRequestQuery<T>(this T item, NameValueCollection query)
        {
            item.GetType().GetProperties().ToList()
                .ForEach(_=>_.SetValue(item, string.IsNullOrWhiteSpace(query[_.Name.ToLower()]) 
                    ? null 
                    : Convert.ChangeType(query[_.Name.ToLower()], Nullable.GetUnderlyingType(_.PropertyType) ?? _.PropertyType)));
        }

        public static void DeliverSerialazedStream<T>(this HttpResponse httpResponse, IEnumerable<T> source, string fileName, string contentType = DefaultType)
        {
            httpResponse.Clear();
            httpResponse.ContentType = contentType;
            var filepath = string.Format(FilePath, fileName);
            httpResponse.AddHeader(ContentDisposition, filepath);
            IWriter writer;
            if (DefaultType.Equals(contentType))
                writer = new WriterToXls();
            else
                writer = new WriterToXml();
            writer.Write(source, httpResponse.OutputStream);
            httpResponse.End();
        }
    }
}