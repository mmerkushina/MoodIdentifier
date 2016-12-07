using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aylien.TextApi;
using MoodIdentifier.AnalysisData.DTO.Response;
using System.Net.Http;
using Newtonsoft.Json;

namespace MoodIdentifier.AnalysisData
{
    public class RepositoryAnalysisData
    {


        public const string AppId = "375d440c74c17eba6676796f88ef68a2abb7fb14";

        public string CheckAnalysis(string text)
        {

            //return string.Format("https://watson-api-explorer.mybluemix.net/alchemy-api/calls/text/TextGetEmotion?apikey={0}&text={1}",
            //       AppId,text);
            return string.Format("https://watson-api-explorer.mybluemix.net/alchemy-api/calls/text/TextGetEmotion?apikey=375d440c74c17eba6676796f88ef68a2abb7fb14&text=i%20am%20tired&outputMode=json");
        }
        public Results GetAnalysis(string text)
        {
            using (var client = new HttpClient())
            {
                var result = client.GetStringAsync(CheckAnalysis(text)).Result;
                return JsonConvert.DeserializeObject<Results>(result);
            }
        }


        /*
            Client client = new Client(" ", "");
          
              Sentiment sentiment = client.Sentiment(text: "I am happy");
            return (sentiment.Polarity.ToString());
            //return ()*/
        // Language language = client.Language(
        //   text: "John is a very good football player!");
        /*
        Console.WriteLine("Sentiment: {0} ({1})", 
                sentiment.Polarity, sentiment.PolarityConfidence);
            Console.WriteLine("Language: {0} ({1})",
                language.Lang, language.Confidence);*/
    }
}
