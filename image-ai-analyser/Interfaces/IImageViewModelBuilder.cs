using image_ai_analyser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace image_ai_analyser.Interfaces
{
    public interface IImageViewModelBuilder
    {
        Task<ImageViewModel> MapToImageModel(string jsonString);
    }
}