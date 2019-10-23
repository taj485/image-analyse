using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using image_ai_analyser.Models;

namespace image_ai_analyser.Models
{
    public class ImageViewModel
    {
        public Description ImageDescriptions { get; set; } = new Description();
        public Caption ImageCaption { get; set; } = new Caption();
    }
    
}