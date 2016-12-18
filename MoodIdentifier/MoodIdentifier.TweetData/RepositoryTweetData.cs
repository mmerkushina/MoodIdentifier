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
            Dictionary<DateTime, List<string>> result = new Dictionary<DateTime, List<string>>();
            var twitterContext = new TwitterContext(user);
            try
            {
                var tweets = from tweet in twitterContext.Status
                             where tweet.Type == StatusType.User &&
                             tweet.ScreenName == screenname &&
                             tweet.Lang == "en" &&
                             tweet.CreatedAt >= begin &&
                             tweet.CreatedAt <= end &&
                             tweet.Count == 200
                             orderby tweet.CreatedAt
                             group tweet.Text by tweet.CreatedAt.Date;

                result = tweets.ToDictionary(t => t.Key, t => t.ToList());
            }
            catch
            {
                throw new Exception("Check the Internet connection or API keys");
            }
            return result;
        }
    }
}
