using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aylien.TextApi;

namespace MoodIdentifier.AnalysisData
{
   public class RepositoryAnalysisData { 
    
        static Client client = new Client("afcc8bcf", "7cceb65d718a77e60f4896cd27bbf912");
        Sentiment sentiment = client.Sentiment(
            text: "John is a very good football player!");
        Language language = client.Language(
            text: "John is a very good football player!");
        /*
        Console.WriteLine("Sentiment: {0} ({1})", 
                sentiment.Polarity, sentiment.PolarityConfidence);
            Console.WriteLine("Language: {0} ({1})",
                language.Lang, language.Confidence);*/
    }
}
