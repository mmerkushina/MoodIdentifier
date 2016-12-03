using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.TweetData.DTO
{
    class Tweet
    {
        public User User { get; set; }
        public string CreatedAt { get; set; }
        public int FavouriteCount { get; set; }
        public int RetweetCount { get; set; }
        public string Text { get; set; }
        public Entity Entities { get; set; }

    }
}
