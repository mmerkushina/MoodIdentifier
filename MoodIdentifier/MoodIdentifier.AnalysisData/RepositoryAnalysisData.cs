using System;
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
using MoodIdentifier.TweetData;

namespace MoodIdentifier.AnalysisData
{
    public class RepositoryAnalysisData
    {
        public const string AppId = "Tonya Key";


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
        


        public float FromStrToFloat(string str)
        {
           return( float.Parse(str.Replace('.', ',')));
            
        }



        public Answer Сomputation(EmotionalsOneDay emotionaloneday,int count) //что-нибудь с этим придумаем и изменим 
        {
            float[] Numbers = new float[] { emotionaloneday.Anger, emotionaloneday.Disgust, emotionaloneday.Fear, emotionaloneday.Joy, emotionaloneday.Sadness };
            float max = Numbers.Max();
            int i = Array.IndexOf(Numbers, max);
            string NameEmotion = "";
            if (i == 0) { NameEmotion = "Anger"; }
            if (i == 1) { NameEmotion = "Disgust"; }
            if (i == 2) { NameEmotion = "Fear"; }
            if (i == 3) { NameEmotion = "Joy"; }
            if (i == 4) { NameEmotion = "Sadness"; }
            Answer answer = new Answer
            {
                Emotion = NameEmotion,
                NumberEmo = max / count
            };
            return (answer);
        }




        public Answer GetAnswer(List<TweetModel> SetOfTweets) //метод выдает самую сильную эмоцию которую считывает с массива из TweetModel. 
        {
            int count = 0;
            List<docEmotions> DataForAnalysis = new List<docEmotions>();
            foreach (TweetModel tweet in SetOfTweets)
            {
                string TextOfTweet = tweet.Text;
                docEmotions RawAnalysis = (GetAnalysis(TextOfTweet).DocEmotions);
                DataForAnalysis.Add(RawAnalysis);
                
            }
            EmotionalsOneDay emotionaloneday = new EmotionalsOneDay();
            foreach (docEmotions OneTweetData in DataForAnalysis) // Суммирует значения каждой эмоции из каждого твитта
            {
                
                emotionaloneday.Anger = emotionaloneday.Anger + FromStrToFloat(OneTweetData.Anger);
                emotionaloneday.Disgust=emotionaloneday.Disgust+ FromStrToFloat(OneTweetData.Disgust);
                emotionaloneday.Fear = emotionaloneday.Fear + FromStrToFloat(OneTweetData.Fear);
                emotionaloneday.Joy = emotionaloneday.Joy + FromStrToFloat(OneTweetData.Joy);
                emotionaloneday.Sadness = emotionaloneday.Sadness + FromStrToFloat(OneTweetData.Sadness);
                count = count + 1;
            }

            return (Сomputation(emotionaloneday, count));
            

        }


        
    }
}
