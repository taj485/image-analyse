using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using image_ai_analyser.Interfaces;
using image_ai_analyser.Models;

namespace image_ai_analyser.ModelBuilder
{
    public class ImageViewModelBuilder : IImageViewModelBuilder
    {

        public async Task<string> MakeAnalysisRequest(string filePath)
        {

            string subscriptionKey = ConfigurationManager.AppSettings["subscriptionKey"];

            string endpoint = ConfigurationManager.AppSettings["endpoint"];

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
        public byte[] ConvertToBytesArray(string file)
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