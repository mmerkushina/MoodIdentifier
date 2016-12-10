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
        public double mainWindowHeight = 350;
        public double mainWindowWidth = 525;
        public double outputdataHeight = 480;
        public double outputdataWidth = 640;
     
        public void ChangePlace()
        {
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth / 2 - mainWindowWidth / 2;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2 - mainWindowHeight / 2;
            this.MinHeight = 350;
            this.MinWidth = 500;
        }

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
            /*
            RepositoryTweetData rtd = new RepositoryTweetData();
            RepositoryAnalysisData rad = new RepositoryAnalysisData();
            foreach (var i in rtd.GetTweets("top10", new DateTime(2015, 10, 15), new DateTime(2015, 11, 6)))
            {
                var a = rad.GetAnalysis(i);
                Console.WriteLine("Anger: {0}, Disqust: {1}, Fear: {2}, Joy: {3}, Sadness: {4}",
                   a.DocEmotions.Anger, a.DocEmotions.Disgust, a.DocEmotions.Fear, a.DocEmotions.Joy, a.DocEmotions.Sadness);
            }*/

            /* //Check for tonya again
            //RepositoryTweetData rtd = new RepositoryTweetData();
            //RepositoryAnalysisData rad = new RepositoryAnalysisData();
            Console.WriteLine("HEY!");
          Console.WriteLine(  rad.GetAnswer(rtd.GetTweetsForT("top10", new DateTime(2015, 10, 15), new DateTime(2015, 11, 6))).Emotion+"    " + rad.GetAnswer(rtd.GetTweetsForT("top10", new DateTime(2015, 10, 15), new DateTime(2015, 11, 6))).NumberEmo);
            */

            ChangePlace();
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
                { //497 на 301 - минимум
                    mainWindowWidth = this.Width;
                    mainWindowHeight = this.Height;
                    this.Left = System.Windows.SystemParameters.PrimaryScreenWidth/2 
                        - (mainWindowWidth + outputdataWidth)/2;
                    this.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2 - outputdataHeight / 2; 
                    var _leftOfTheScreen = System.Windows.SystemParameters.PrimaryScreenWidth;
                    var _topOfTheScreen = System.Windows.SystemParameters.PrimaryScreenHeight;
                    OutputData _outputDataWindow = new OutputData();
                    _outputDataWindow.EventOutputDataClosed += ChangePlace;
                    _outputDataWindow.Left = _leftOfTheScreen / 2 - (mainWindowWidth + outputdataWidth) / 2 + mainWindowWidth;
                    _outputDataWindow.Top = _topOfTheScreen/2 - outputdataHeight / 2;
                    _outputDataWindow.MinHeight = 350;
                    _outputDataWindow.MinWidth = 210;
                    _outputDataWindow.Show();
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

        private void Info(object sender, RoutedEventArgs e)
        {
            textBoxInfo.Visibility = Visibility.Visible;
            ButtonInfo.IsEnabled = true;
            ButtonInfo.Visibility = Visibility.Visible;
        }

        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            textBoxInfo.Visibility = Visibility.Hidden;
            ButtonInfo.IsEnabled = false;
            ButtonInfo.Visibility = Visibility.Hidden;
            
        }
    }
}