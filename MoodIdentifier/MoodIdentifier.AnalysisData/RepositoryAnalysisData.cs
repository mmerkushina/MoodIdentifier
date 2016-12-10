﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aylien.TextApi;
using MoodIdentifier.AnalysisData.DTO.Response;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web;
using MoodIdentifier.AnalysisData.DTO.Response.TweetData;

namespace MoodIdentifier.AnalysisData
{
    public class RepositoryAnalysisData
    {
        public const string AppId = "";


        public string CheckAnalysis(string text)
        {           
            text= HttpUtility.UrlEncode(text);
            return string.Format("https://watson-api-explorer.mybluemix.net/alchemy-api/calls/text/TextGetEmotion?apikey={0}&text={1}&outputMode=json",AppId,text);
        }
        public Results GetAnalysis(string text)
        {
            using (var client = new HttpClient())
            {
                var result = client.GetStringAsync(CheckAnalysis(text)).Result;
                return JsonConvert.DeserializeObject<Results>(result);
            }
        }
        public void GetAnswer(List<TweetModel> SetOfTweets)
        {
            List<docEmotions> DataForAnalysis = new List<docEmotions>();
            foreach (TweetModel tweet in SetOfTweets)
            {
                string TextOfTweet = tweet.Text;
                docEmotions RawAnalysis = (GetAnalysis(TextOfTweet).DocEmotions);
                DataForAnalysis.Add(RawAnalysis);
                
            }


        }


        
    }
}
