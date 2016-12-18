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
using MoodIdentifier.AnalysisData.DTO.Response;
using System.Windows.Threading;

namespace MoodIdentifier.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer animationTimer = new DispatcherTimer();
        public double mainWindowHeight = 350;
        public double mainWindowWidth = 525;
        public double outputdataHeight = 480;
        public double outputdataWidth = 640;
        
        public void Start()
        {
            process_background.Visibility = Visibility.Visible;
            C0.Visibility = Visibility.Visible;
            C1.Visibility = Visibility.Visible;
            C2.Visibility = Visibility.Visible;
            C3.Visibility = Visibility.Visible;
            C4.Visibility = Visibility.Visible;
            C5.Visibility = Visibility.Visible;
            C6.Visibility = Visibility.Visible;
            C7.Visibility = Visibility.Visible;
            C8.Visibility = Visibility.Visible;
            animationTimer.Tick += HandleAnimationTick;
            animationTimer.Start();
        }

        public void Stop()
        {
            animationTimer.Stop();
            C0.Visibility = Visibility.Hidden;
            C1.Visibility = Visibility.Hidden;
            C2.Visibility = Visibility.Hidden;
            C3.Visibility = Visibility.Hidden;
            C4.Visibility = Visibility.Hidden;
            C5.Visibility = Visibility.Hidden;
            C6.Visibility = Visibility.Hidden;
            C7.Visibility = Visibility.Hidden;
            C8.Visibility = Visibility.Hidden;
            process_background.Visibility = Visibility.Hidden;
            animationTimer.Tick -= HandleAnimationTick;
        }

        private void HandleAnimationTick(object sender, EventArgs e)
        {
            SpinnerRotate.Angle = (SpinnerRotate.Angle + 36) % 360;
        }

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
            animationTimer.Interval = new System.TimeSpan(0, 0, 0, 0, 70);
            ChangePlace();
            InitializeComponent();
        }

        private async void Buttion_Start_analyzing_Click(object sender, RoutedEventArgs e)
        {
            //TextBox_Login.ToolTip += TextBox_Login.Text;
            string login = TextBox_Login.Text;
            DateTime? firstdate = DatePickerFirst.SelectedDate;
            DateTime? seconddate = DatePickerSecond.SelectedDate;
            Validation valid = new Validation();
            
            if (valid.IsValid(firstdate,seconddate))
            {
                //seconddate = seconddate.Value.AddDays(1);
                if (valid.IsValid(login))
                {

                    RepositoryTweetData rtd = new RepositoryTweetData();
                    RepositoryAnalysisData rad = new RepositoryAnalysisData();
                    Start();
                    //Downloading tweets
                    Dictionary<DateTime, List<string>> tweets = new Dictionary<DateTime, List<string>>();
                    tweets = rtd.GetTweets(login, (DateTime)firstdate, (DateTime)seconddate);

                    //Downloading emotion of each tweet
                    var analyzed = await rad.GetAnswer(tweets);

                    //Creating list in format of datagrid
                    List<DataToOutput> outputdatalist = new List<DataToOutput>();
                    foreach (var item in analyzed)
                    {
                        outputdatalist.Add(new DataToOutput
                        {
                            Date = item.Key.Date.ToString("d"),
                            MainEmotion = item.Value.Emotion
                        });
                    }
                    //DataGridTemplateColumn columnforimages = new DataGridTemplateColumn();
                    //columnforimages.Header = "Emotion";
                    //outputDataWindow.dataGridOutput.Columns.Add(columnforimages);
                    OutputData outputDataWindow = new OutputData();
                    outputDataWindow.EventOutputDataClosed += ChangePlace;
                    mainWindowWidth = this.Width;
                    mainWindowHeight = this.Height;
                    this.Left = System.Windows.SystemParameters.PrimaryScreenWidth / 2
                        - (mainWindowWidth + outputdataWidth) / 2;
                    this.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2 - outputdataHeight / 2;
                    var leftOfTheScreen = System.Windows.SystemParameters.PrimaryScreenWidth;
                    var topOfTheScreen = System.Windows.SystemParameters.PrimaryScreenHeight;
                    outputDataWindow.Left = leftOfTheScreen / 2 - (mainWindowWidth + outputdataWidth) / 2 + mainWindowWidth;
                    outputDataWindow.Top = topOfTheScreen / 2 - outputdataHeight / 2;
                    outputDataWindow.MinHeight = 350;
                    outputDataWindow.MinWidth = 210;
                    Stop();
                    outputDataWindow.Show();

                    outputDataWindow.dataGridOutput.ItemsSource = outputdatalist;
                }
                else
                {
                    MessageBox.Show("Please, enter correct login");
                }
            }
            else
            {
                MessageBox.Show("Please, enter correct dates");
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