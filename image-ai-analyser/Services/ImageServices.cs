using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace image_ai_analyser.Services
{
    public class ImageServices
    {
        public async Task<string> MakeAnalysisRequestAysync(string filePath)
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

                response = await client.PostAsync(uri, content);
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            return jsonString.ToString();
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