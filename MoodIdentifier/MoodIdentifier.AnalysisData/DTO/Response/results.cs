using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.AnalysisData.DTO.Response
{
   public class Results

    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("usage")]
        public string Usage { get; set; }

        [JsonProperty("totalTransactions")]
        public string TotalTransactions { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("docEmotions")]
        public docEmotions DocEmotions{ get; set; }

    }
}
