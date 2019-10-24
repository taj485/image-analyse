using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using image_ai_analyser.Models;
using Newtonsoft.Json;

namespace image_ai_analyser.Models
{
    public class ImageViewModel
    {
        [JsonProperty("description")]
        public Description Descriptions { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public string ImageString { get; set; }
    }
    
}