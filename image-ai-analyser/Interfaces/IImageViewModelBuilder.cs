using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace image_ai_analyser.Interfaces
{
    public interface IImageViewModelBuilder
    {
        Task<string> MakeAnalysisRequest(string filePath);
        byte[] ConvertToBytesArray(string file);
    }
}