using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.AnalysisData.DTO.Response
{
   public class docEmotions
    {
        [JsonProperty("anger")]
        public string Anger { get; set; }
        [JsonProperty("disgust")]
        public string Disgust { get; set; }
        [JsonProperty("fear")]
        public string Fear { get; set; }
        [JsonProperty("joy")]
        public string Joy { get; set; }
        [JsonProperty("sadness")]
        public string Sadness { get; set; }
    }
}
