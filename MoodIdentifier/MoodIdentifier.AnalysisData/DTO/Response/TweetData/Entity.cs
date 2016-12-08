using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.AnalysisData.DTO.Response.TweetData
{
    public class Entity
    {
        [JsonProperty("hashtags")]
        public IEnumerable<Hashtag> Hashtags { get; set; }
    }
}
