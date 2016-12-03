using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.TweetData.DTO
{
    class Entity
    {
        [JsonProperty("hashtags")]
        public Hashtag[] Hashtags { get; set; }
    }
}
