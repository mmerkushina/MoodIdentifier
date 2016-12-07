using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aylien.TextApi;

namespace MoodIdentifier.AnalysisData
{
   public class RepositoryAnalysisData { 
    
        public string checkAnalysis()
        {
            Client client = new Client("YourApplicationID", "YourApplicationKey");

            Sentiment sentiment = client.Sentiment(text: "John is a very good football player!");
            return (sentiment.Polarity.ToString());
            //return ()
        }
        
       // Language language = client.Language(
         //   text: "John is a very good football player!");
        /*
        Console.WriteLine("Sentiment: {0} ({1})", 
                sentiment.Polarity, sentiment.PolarityConfidence);
            Console.WriteLine("Language: {0} ({1})",
                language.Lang, language.Confidence);*/
    }
}
