using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.TweetData
{
    public class TweetModel
    {
        public string Text { get; set; }
        //public List<string> Hashtags { get; set; }
        public int? Favourites { get; set; }
        public int? Retweets { get; set; }
    }
}
