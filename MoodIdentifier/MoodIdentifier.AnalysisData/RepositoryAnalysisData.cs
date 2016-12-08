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
        public Results GetAnalysis(string text)
        {
            using (var client = new HttpClient())
            {
                var result = client.GetStringAsync(CheckAnalysis(text)).Result;
                return JsonConvert.DeserializeObject<Results>(result);
            }
        }


        
    }
}
