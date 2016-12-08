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
                ConsumerKey = "",
                ConsumerSecret = "",
                AccessToken = "",
                AccessTokenSecret = ""
            }
        };
        public List<TweetModel> TweetsCollection { get; set; }
        public List<string> GetTweets(string screenname, DateTime begin, DateTime end)
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
            List<string> TweetText = new List<string>();
            foreach (var t in TweetsCollection)
                TweetText.Add(t.Text);
            return TweetText;
        }
    }
}
