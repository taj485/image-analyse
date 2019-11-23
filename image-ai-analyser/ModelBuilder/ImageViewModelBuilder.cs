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
using image_ai_analyser.Services;

namespace image_ai_analyser.ModelBuilder
{
    public class ImageViewModelBuilder : IImageViewModelBuilder
    {
        private readonly ImageServices _imageServices;

        public ImageViewModelBuilder(ImageServices imageServices)
        {
            _imageServices = imageServices;
        }

        public async Task<ImageViewModel> MapToImageModel(string filePath)
        {
            string jsonString = await _imageServices.MakeAnalysisRequestAysync(filePath);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ImageViewModel>(jsonString);
        }
    }
}