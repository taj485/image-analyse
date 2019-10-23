using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using Newtonsoft.Json;

namespace image_ai_analyser.Models
{
    public class Caption
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("confidence")]
        public int Confidence { get; set; }
    }
}