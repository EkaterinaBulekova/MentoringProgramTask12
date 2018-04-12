using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;

namespace NorthwindHttpHandler.Writers
{
    public class WriterToXls : IWriter
    {
        public void Write<T>(IEnumerable<T> source, Stream outStream)
        {
            var xlBook = new XLWorkbook();
            var sheet = xlBook.Worksheets.Add("report");
            sheet.Cell(1,1).InsertData(GetDataTable(source));
            using (outStream)
            {
                xlBook.SaveAs(outStream);
            }
        }

        private IEnumerable<PropertyInfo> GetTypeProperties<T>() => 
            typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(_ =>!_.GetGetMethod().IsVirtual).ToList();

        private IEnumerable<string[]> GetDataTable<T>(IEnumerable<T> source)
        {
            var data = source.ToList();
            var props = GetTypeProperties<T>().ToList();
            var tableData = new List<string[]>
            {
                props.Select(_ => _.Name).ToArray()
            };
            data.ForEach(_=> tableData.Add(props.Select(p => p.GetValue(_)?.ToString() ?? "").ToArray()));

            return tableData;
        }
    }
}