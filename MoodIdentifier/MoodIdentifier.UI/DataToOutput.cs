using MoodIdentifier.AnalysisData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.UI
{
    public class DataToOutput
    {
        public DateTime Date { get; set; }
        public string Emotion { get; set; }
        public float NumberEmo { get; set; }
    }
}
