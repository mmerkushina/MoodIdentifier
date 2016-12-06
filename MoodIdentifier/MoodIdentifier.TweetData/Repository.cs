using LinqToTwitter;
using MoodIdentifier.TweetData.DTO.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.TweetData
{
    public class Repository
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
        public void GetTweets(string screenname, DateTime begin, DateTime end)
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
                             //Hashtags = from t in tweet.Entities.HashTagEntities
                             //           select t.Tag
                             //           .ToList()
                         };
            TweetsCollection = tweets.ToList();
        }
    }
}
