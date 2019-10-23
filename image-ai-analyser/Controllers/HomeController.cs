using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using image_ai_analyser.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace image_ai_analyser.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // Post send api
        [HttpPost]
        public async Task<ActionResult> ProcessImage(HttpPostedFileBase file)
        {

            byte[] byteData = ConvertToBytes(file);

            string subscriptionKey = "4c0a252e509347d8b33ff4b18ee5fc13";

            string endpoint = "https://uksouth.api.cognitive.microsoft.com/";

            string uriBase = endpoint + "vision/v2.1/analyze";

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add(
                "Ocp-Apim-Subscription-Key", subscriptionKey);

            string requestParameters =
                "visualFeatures=Categories,Description,Color";

            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                // Asynchronously call the REST API method.
                response = await client.PostAsync(uri, content);
            }

            string jsonString = await response.Content.ReadAsStringAsync();

            List<ImageViewModel> model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ImageViewModel>>(jsonString);

            return View("imageResult", model);
        }


        public ActionResult ImageResult()
        {
            return View();
        }

        private static byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            int fileSizeInBytes = file.ContentLength;
            byte[] data = null;
            using (var br = new BinaryReader(file.InputStream))
            {
                data = br.ReadBytes(fileSizeInBytes);
            }

            return data;
        }
    }
}