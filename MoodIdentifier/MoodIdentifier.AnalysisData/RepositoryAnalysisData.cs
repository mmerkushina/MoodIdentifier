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

using MoodIdentifier.TweetData;

namespace MoodIdentifier.AnalysisData
{
    public class RepositoryAnalysisData
    {
        public const string AppId = "3595b2002fc8cae28f33752ab9d76d1a8333149e";


        public string CheckAnalysis(string text)
        {           
            text= HttpUtility.UrlEncode(text);
           
            return string.Format("https://watson-api-explorer.mybluemix.net/alchemy-api/calls/text/TextGetEmotion?apikey={0}&text={1}&outputMode=json",AppId,text);
        }



        public float FromStrToFloat(string str)
        {
           return( float.Parse(str.Replace('.', ',')));
            
        }



        public Answer Сomputation(EmotionOneDay emotionaloneday,int count) //что-нибудь с этим придумаем и изменим 
        {
            float[] Numbers = new float[] { emotionaloneday.Anger, emotionaloneday.Disgust, emotionaloneday.Fear, emotionaloneday.Joy, emotionaloneday.Sadness };
            float max = Numbers.Max();
            int i = Array.IndexOf(Numbers, max);
            string NameEmotion = "";
            switch (i)
            {
                case (0):
                    NameEmotion = "Anger";
                    break;
                case (1):
                    NameEmotion = "Disgust";
                    break;
                case (2):
                    NameEmotion = "Fear";
                    break;
                case (3):
                    NameEmotion = "Joy";
                    break;
                case (4):
                    NameEmotion = "Sadness";
                    break;

            }
            /*
            if (i == 0) { NameEmotion = "Anger"; }
            if (i == 1) { NameEmotion = "Disgust"; }
            if (i == 2) { NameEmotion = "Fear"; }
            if (i == 3) { NameEmotion = "Joy"; }
            if (i == 4) { NameEmotion = "Sadness"; }*/
            Answer answer = new Answer
            {
                Emotion = NameEmotion,
                NumberEmo = max / count
            };
            return (answer);
        }



        
        public async  Task<Dictionary<DateTime, Answer>> GetAnswer(Dictionary<DateTime,List<string>> SetOfTweets)  
        {
            Dictionary<DateTime, Answer> Answers = new Dictionary<DateTime, Answer>();
            List<Answer> SetAnalysisDate = new List<Answer>();
            
            
            
            foreach (KeyValuePair<DateTime, List<string>> keyValue in SetOfTweets)
            {
                int count = 0;
                List<docEmotions> DataForAnalysis = new List<docEmotions>();
                List<string> SetOnedayTweet = keyValue.Value;
                try
                {
                    using (var client = new HttpClient())
                    {
                        



                        foreach (string TextOfTweet in SetOnedayTweet)
                        {
                            var raw = await client.GetStringAsync(CheckAnalysis(TextOfTweet));
                            var result = JsonConvert.DeserializeObject<Results>(raw);
                            var c = result;
                            

                            docEmotions RawAnalysis = c.DocEmotions;

                            DataForAnalysis.Add(RawAnalysis);
                            
                        }
                    }
                }
                catch
                {
                    throw new Exception("Too many requests. Please, buy the full version of MyBlueMix.");
                }

                EmotionOneDay emotionaloneday = new EmotionOneDay();
                try
                {
                    foreach (docEmotions OneTweetData in DataForAnalysis)
                    {

                        if (OneTweetData.Anger != null) { emotionaloneday.Anger += FromStrToFloat(OneTweetData.Anger); }
                        if (OneTweetData.Disgust != null) { emotionaloneday.Disgust += FromStrToFloat(OneTweetData.Disgust); }
                        if (OneTweetData.Fear != null) { emotionaloneday.Fear += FromStrToFloat(OneTweetData.Fear); }
                        if (OneTweetData.Joy != null) { emotionaloneday.Joy += FromStrToFloat(OneTweetData.Joy); }
                        if (OneTweetData.Sadness != null) { emotionaloneday.Sadness += FromStrToFloat(OneTweetData.Sadness); }
                        count = count + 1;
                    }
                }
                catch
                {
                    throw new Exception("Text of tweet is to short for analysis!");
                }
                Answer a = Сomputation(emotionaloneday, count);
                Answers.Add(keyValue.Key,a);

            }


            return (Answers); //метод возвращает словарь из (ключ - дата)-(начение-эмоция+цифраэмоции) по всем датам
        }








        public async Task<Dictionary<DateTime, ClassForAnalysis>> GetAnswer2 (Dictionary<DateTime, List<string>> SetOfTweets) 
        {
            Dictionary<DateTime, ClassForAnalysis> Answers2 = new Dictionary<DateTime, ClassForAnalysis>();
            List<Answer> SetAnalysisDate = new List<Answer>();



            foreach (KeyValuePair<DateTime, List<string>> keyValue in SetOfTweets)
            {
                int count = 0;
                List<docEmotions> DataForAnalysis = new List<docEmotions>();
                List<string> SetOnedayTweet = keyValue.Value;
                try
                {
                    using (var client = new HttpClient())
                    {





                        foreach (string TextOfTweet in SetOnedayTweet)
                        {
                            var raw = await client.GetStringAsync(CheckAnalysis(TextOfTweet));
                            var result = JsonConvert.DeserializeObject<Results>(raw);
                            var c = result;


                            docEmotions RawAnalysis = c.DocEmotions;

                            DataForAnalysis.Add(RawAnalysis);

                        }
                    }
                }
                catch
                {
                    throw new Exception("Too many requests. Please, buy the full version of MyBlueMix.");
                }

                EmotionOneDay emotionaloneday = new EmotionOneDay();
                try
                {
                    foreach (docEmotions OneTweetData in DataForAnalysis)
                    {

                        emotionaloneday.Anger = emotionaloneday.Anger + FromStrToFloat(OneTweetData.Anger);
                        emotionaloneday.Disgust = emotionaloneday.Disgust + FromStrToFloat(OneTweetData.Disgust);
                        emotionaloneday.Fear = emotionaloneday.Fear + FromStrToFloat(OneTweetData.Fear);
                        emotionaloneday.Joy = emotionaloneday.Joy + FromStrToFloat(OneTweetData.Joy);
                        emotionaloneday.Sadness = emotionaloneday.Sadness + FromStrToFloat(OneTweetData.Sadness);
                        count = count + 1;
                    }
                }
                catch
                {
                    throw new Exception("Text of tweet is to short for analysis!");
                }
                ClassForAnalysis b = new ClassForAnalysis
                {
                    CountTweets = count,
                    AllEmotion = emotionaloneday
                };
                Answers2.Add(keyValue.Key, b);
            }


            return (Answers2); 
        }




    }
}
