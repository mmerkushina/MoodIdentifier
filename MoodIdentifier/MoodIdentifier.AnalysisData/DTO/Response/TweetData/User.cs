using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.AnalysisData.DTO.Response.TweetData
{
    public class User
    {
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

    }
}
