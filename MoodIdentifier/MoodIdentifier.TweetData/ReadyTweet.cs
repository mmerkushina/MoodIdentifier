using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoodIdentifier.TweetData.DTO;

namespace MoodIdentifier.TweetData
{
    class ReadyTweet
    {
        
        public string User { get; set; }
        public string Text { get; set; }
        public DateTime PostedAt { get; set; }
        public List<string> Hashtags { get; set; }
        public int FavouriteCount { get; set; }
        public int RetweetedCount { get; set; }
    }
}
