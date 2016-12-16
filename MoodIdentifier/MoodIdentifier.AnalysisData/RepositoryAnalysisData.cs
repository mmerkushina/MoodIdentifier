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
        public const string AppId = "375d440c74c17eba6676796f88ef68a2abb7fb14";


        public string CheckAnalysis(string text)
        {           
            text= HttpUtility.UrlEncode(text);
           
            return string.Format("https://watson-api-explorer.mybluemix.net/alchemy-api/calls/text/TextGetEmotion?apikey={0}&text={1}&outputMode=json",AppId,text);
        }



        public async Task<Results> GetAnalysis(string text)
        {
           
            using (var client = new HttpClient())
            {
                System.Threading.Thread.Sleep(4000);
                var raw = await client.GetStringAsync(CheckAnalysis(text));
                 var result = JsonConvert.DeserializeObject<Results>(raw);
                
                return result;
            }
        }


        //работай прога
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



        
        public async  Task<Dictionary<DateTime, Answer>> GetAnswer(Dictionary<DateTime,List<string>> SetOfTweets)  
        {
            Dictionary<DateTime, Answer> Answers = new Dictionary<DateTime, Answer>();
            List<Answer> SetAnalysisDate = new List<Answer>();
            
            
            
            foreach (KeyValuePair<DateTime, List<string>> keyValue in SetOfTweets)
            {
                int count = 0;
                List<docEmotions> DataForAnalysis = new List<docEmotions>();
                List<string> SetOnedayTweet = keyValue.Value;
                Console.WriteLine("Тута");
                Console.WriteLine("Тута2");
                foreach (string TextOfTweet in SetOnedayTweet)
                {

                    System.Threading.Thread.Sleep(4000);
                    var b = await GetAnalysis(TextOfTweet);
                   
                    docEmotions RawAnalysis = b.DocEmotions;
                    
                    DataForAnalysis.Add(RawAnalysis);
                   //а
                }

                EmotionOneDay emotionaloneday = new EmotionOneDay();

                foreach (docEmotions OneTweetData in DataForAnalysis) 
                {

                    emotionaloneday.Anger = emotionaloneday.Anger + FromStrToFloat(OneTweetData.Anger);
                    emotionaloneday.Disgust = emotionaloneday.Disgust + FromStrToFloat(OneTweetData.Disgust);
                    emotionaloneday.Fear = emotionaloneday.Fear + FromStrToFloat(OneTweetData.Fear);
                    emotionaloneday.Joy = emotionaloneday.Joy + FromStrToFloat(OneTweetData.Joy);
                    emotionaloneday.Sadness = emotionaloneday.Sadness + FromStrToFloat(OneTweetData.Sadness);
                    count = count + 1;
                }

                Answer a = Сomputation(emotionaloneday, count);
                Answers.Add(keyValue.Key,a);

            }


            return (Answers); //метод возвращает словарь из (ключ - дата)-(начение-эмоция+цифраэмоции) по всем датам
        }

        
         


    }
}
