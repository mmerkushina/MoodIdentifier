using MoodIdentifier.AnalysisData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.UI
{
    public class Converter
    {
        public List<DataToOutput> FromDictionaryToList(Dictionary<DateTime, ClassForAnalysis> dictionary)
        {
            List<DataToOutput> outputlist = new List<DataToOutput>();
            DataToOutput temp = new DataToOutput();
            RepositoryAnalysisData rad = new RepositoryAnalysisData();
            foreach (var item in dictionary.Keys)
            { 
                outputlist.Add(new DataToOutput { Date = item.ToString("d"), MainEmotion = rad.Сomputation(dictionary[item].AllEmotion, dictionary[item].CountTweets).Emotion });
            }
            return (outputlist);
        }
    }
}
