using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MoodIdentifier.TweetData;
using MoodIdentifier.AnalysisData;
using System.Net;
using System.IO;
using LinqToTwitter;




namespace MoodIdentifier.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Repository r = new Repository();
            // FOR MASHA TO CHECK
            //r.GetTweets("top10", new DateTime(2015, 10, 15), new DateTime(2015, 11, 6));
            //foreach (var i in r.TweetsCollection)
            //{
            //    Console.WriteLine(i.Text);
            //}

            ///for Tonya 
            /*
            MoodIdentifier.AnalysisData.RepositoryAnalysisData repo = new MoodIdentifier.AnalysisData.RepositoryAnalysisData();
            Console.WriteLine("Sentiment: {0}",repo.checkAnalysis());
           string text2 = "I am happy";


            var a = repo.GetAnalysis(text2);
            Console.WriteLine(a.DocEmotions.Joy);
               */
            RepositoryTweetData rtd = new RepositoryTweetData();
            RepositoryAnalysisData rad = new RepositoryAnalysisData();
            foreach (var i in rtd.GetTweets("top10", new DateTime(2015, 10, 15), new DateTime(2015, 11, 6)))
            {
                var a = rad.GetAnalysis(i);
                Console.WriteLine("Anger: {0}, Disqust: {1}, Fear: {2}, Joy: {3}, Sadness: {4}",
                    a.DocEmotions.Anger, a.DocEmotions.Disgust, a.DocEmotions.Fear, a.DocEmotions.Joy, a.DocEmotions.Sadness);
            }
            InitializeComponent();
        }

        private void Buttion_Start_analyzing_Click(object sender, RoutedEventArgs e)
        {
            string _login = TextBox_Login.Text;
            DateTime? _firstdate = DatePickerFirst.SelectedDate;
            DateTime? _seconddate = DatePickerSecond.SelectedDate;
            Validation valid = new Validation();
            if (valid.IsValid(_firstdate) && valid.IsValid(_seconddate))
            {
                if (valid.IsValid(_login))
                {
                    OutputData outputdatawindow = new OutputData();
                    outputdatawindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите корректный логин");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите конкретные данные в поле дата");
            }
        }
    }
}