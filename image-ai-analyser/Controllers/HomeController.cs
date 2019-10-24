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

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Server.MapPath("~/images"), fileName);
                file.SaveAs(filePath);
                string jsonString = await MakeAnalysisRequest(filePath);

                List<ImageViewModel> model = new List<ImageViewModel>();

                model.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<ImageViewModel>(jsonString));
                model.First().Image = file; 

                string localPath = "/images/";
                string localPathToImage = localPath + fileName;
                model.First().ImageString = localPathToImage;

                return View("ImageResult", model);
            }

            return View();
        }



        public ActionResult ImageResult()
        {
            return View();
        }

        private async Task<string> MakeAnalysisRequest(string filePath)
        {

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

            byte[] byteData = ConvertToBytesArray(filePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                // Asynchronously call the REST API method.
                response = await client.PostAsync(uri, content);
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;

        }


        private byte[] ConvertToBytesArray(string file)
        {
            using (FileStream fileStream =
                new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
    }
}
