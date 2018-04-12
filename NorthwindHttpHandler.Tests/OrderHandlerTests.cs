using System;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NorthwindHttpHandler.Tests
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly string xmlFileName = "test.xml";
        private readonly string xlsFileName = "test.xlsx";
        private readonly string xmlAppAccept = "application/xml";
        private readonly string xlsAppAccept = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private readonly string baseAddress = "http://localhost:57873/report";
        private readonly KeyValuePair<string, string>[] _testCustomerTake =
        {
            new KeyValuePair<string, string>("customer", "VINET"),
            new KeyValuePair<string, string>("take", "2")
        };
        private readonly KeyValuePair<string, string>[] _testCustomerDateTo =
        {
            new KeyValuePair<string, string>("customer", "VINET"),
            new KeyValuePair<string, string>("dateto", "11/11/1997")
        };
        private readonly KeyValuePair<string, string>[] _testCustomerDateFrom =
        {
            new KeyValuePair<string, string>("customer", "VINET"),
            new KeyValuePair<string, string>("datefrom", "11/11/1997")
        };

        private readonly KeyValuePair<string, string>[] _testCustomerSkip =
        {
            new KeyValuePair<string, string>("customer", "VINET"),
            new KeyValuePair<string, string>("skip", "2")
        };


        [TestMethod]
        public void TestMethodGetXls()
        {
            var file = new FileStream(xlsFileName, FileMode.Create);
            ClientGetRequest(file, xlsAppAccept,_testCustomerTake);
        }

        [TestMethod]
        public void TestMethodGetXml()
        {
            var file = new FileStream(xmlFileName, FileMode.Create);
            ClientGetRequest(file, xmlAppAccept, _testCustomerTake);
        }

        [TestMethod]
        public void TestMethodPostXml()
        {
            var file = new FileStream(xmlFileName, FileMode.Create);
            ClientPostRequest(file, xmlAppAccept, _testCustomerTake);
        }

        [TestMethod]
        public void TestMethodPostXls()
        {
            var file = new FileStream(xlsFileName, FileMode.Create);
            ClientPostRequest(file, xlsAppAccept, _testCustomerTake);
        }

        [TestMethod]
        public void TestMethodWithDateGetXls()
        {
            var file = new FileStream(xlsFileName, FileMode.Create);
            ClientGetRequest(file, xlsAppAccept, _testCustomerDateTo);
        }

        [TestMethod]
        public void TestMethodWithDateGetXml()
        {
            var file = new FileStream(xmlFileName, FileMode.Create);
            ClientGetRequest(file, xmlAppAccept, _testCustomerDateTo);
        }

        [TestMethod]
        public void TestMethodWithDatePostXml()
        {
            var file = new FileStream(xmlFileName, FileMode.Create);
            ClientPostRequest(file, xmlAppAccept, _testCustomerDateFrom);
        }

        [TestMethod]
        public void TestMethodWithDatePostXls()
        {
            var file = new FileStream(xlsFileName, FileMode.Create);
            ClientPostRequest(file, xlsAppAccept, _testCustomerDateFrom);
        }

        [TestMethod]
        public void TestMethodWithSkipGetXls()
        {
            var file = new FileStream(xlsFileName, FileMode.Create);
            ClientGetRequest(file, xlsAppAccept, _testCustomerSkip);
        }

        [TestMethod]
        public void TestMethodWithSkipPostXml()
        {
            var file = new FileStream(xmlFileName, FileMode.Create);
            ClientPostRequest(file, xmlAppAccept, _testCustomerSkip);
        }


        private void ClientPostRequest(Stream stream, string accept, params KeyValuePair<string, string>[] formKeyValuePairs)
        {
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));
                var stringContent = new FormUrlEncodedContent(formKeyValuePairs);
                client.PostAsync(baseAddress, stringContent).Result
                    .Content.ReadAsStreamAsync().Result
                    .CopyTo(stream);
            }
        }

        private void ClientGetRequest(Stream stream, string accept, params KeyValuePair<string, string>[] paramKeyValuePairs)
        {
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));
                client.GetAsync(baseAddress+GetQueryParams(paramKeyValuePairs)).Result
                    .Content.ReadAsStreamAsync().Result
                    .CopyTo(stream);
            }
        }

        private string GetQueryParams(params KeyValuePair<string, string>[] queryParams)
        {
            var paramsString = string.Empty;
            if (queryParams.Length <= 0) return paramsString;
            paramsString += "?";
            foreach (var param in queryParams)
            {
                if (paramsString != "?") paramsString += "&";
                paramsString += string.Format($"{param.Key}={param.Value}");
            }

            return paramsString;
        }
    }
}
