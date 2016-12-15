using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.TweetData
{
    public class RepositoryTweetData
    {
        private SingleUserAuthorizer user = new SingleUserAuthorizer
        {
            CredentialStore = new SingleUserInMemoryCredentialStore
            {
                ConsumerKey = "BSbTPhanWTTQ6NKcLLgtYpbL9",
                ConsumerSecret = "NoN1fU2sthF4unDCTnpxqQ8RNhqcYp8LM7vWUPUC7Lv10uzC3a",
                AccessToken = "470968737-cC3qTBWWcKhqZfgcRVIpB4bwYwDWgsFISfUtDjhp",
                AccessTokenSecret = "2PLKJ49tcSILy8qQsQfxMpUqDzmJzYqDxbClDTqtOQszQ"

            }
        };
        public List<TweetModel> TweetsCollection { get; set; }
        public Dictionary<DateTime,List<string>> GetTweets(string screenname, DateTime begin, DateTime end)
        {
            var twitterContext = new TwitterContext(user);
            List<List<string>> result = new List<List<string>>();
            var tweets = from tweet in twitterContext.Status
                         where tweet.Type == StatusType.User &&
                         tweet.ScreenName == screenname &&
                         tweet.Lang == "en" &&
                         tweet.CreatedAt >= begin &&
                         tweet.CreatedAt <= end &&
                         tweet.Count == 200
                         orderby tweet.CreatedAt
                         group tweet.Text by tweet.CreatedAt.Date;

            return tweets.ToDictionary(t => t.Key, t => t.ToList());
        }
        /* //Метод для тестирования без json Тоня
        public List<TweetModel> GetTweetsForT(string screenname, DateTime begin, DateTime end)
        {
            var twitterContext = new TwitterContext(user);

            var tweets = from tweet in twitterContext.Status
                         where tweet.Type == StatusType.User &&
                         tweet.ScreenName == screenname &&
                         tweet.Lang == "en" &&
                         tweet.CreatedAt >= begin &&
                         tweet.CreatedAt <= end &&
                         tweet.Count == 200
                         orderby tweet.CreatedAt
                         //select tweet;
                         select new TweetModel
                         {
                             Text = tweet.Text,
                             Favourites = tweet.FavoriteCount,
                             Retweets = tweet.RetweetCount
                         };


            TweetsCollection = tweets.ToList();
           // List<string> TweetText = new List<string>();
            //foreach (var t in TweetsCollection)
              //  TweetText.Add(t.Text);
            return TweetsCollection;
        }*/
    }
}
