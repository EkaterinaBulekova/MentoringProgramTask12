using System.Linq;
using System.Web;
using DALEntetyFW.Abstract;
using Ninject;
using Ninject.Web;

namespace NorthwindHttpHandler
{
    public class OrdersHandler : HttpHandlerBase
    {
        private readonly string[] _xmlContent = {"application/xml", "text/xml"};
        private const string XmlFilename = "report.xml";
        private const string XlsxFilename = "report.xlsx";
        private const string RequestPrefix = "/report";
        private const string MethodGet = "get";
        private const string MethodPost = "post";
        private const string Accept = "Accept";


        [Inject]
        public IRepository<IFilter> Repository { get; set; }

        public override bool IsReusable => true;

        protected override void DoProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            if (!request.Url.PathAndQuery.ToLower().Contains(RequestPrefix)) return;

            if (request.HttpMethod.ToLower().Equals(MethodGet))
                Repository.Filter.GetFilterFromRequestQuery(request.QueryString);
            if (request.HttpMethod.ToLower().Equals(MethodPost))
                Repository.Filter.GetFilterFromRequestQuery(request.Form);
            var orders = Repository.GetOrdersByFilter().ToList();
            var accept = request.Headers[Accept];
            if (_xmlContent.Any(_=> accept.Contains(_)))
                response.DeliverSerialazedStream(orders, XmlFilename, _xmlContent[0]);
            else
                response.DeliverSerialazedStream(orders, XlsxFilename);
        }
    }
}

