using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;


namespace image_ai_analyser.Models
{
    public class Description
    {
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
        public List<Caption> Captions { get; set; }
    }
}