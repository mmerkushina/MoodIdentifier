using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.AnalysisData.DTO.Response.TweetData
{
    public class Tweet
    {
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("favorite_count")]
        public int FavouriteCount { get; set; }
        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("entities")]
        public Entity Entities { get; set; }

    }
}
