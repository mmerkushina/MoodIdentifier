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
using MoodIdentifier.TweetData.DTO;
using MoodIdentifier.TweetData;
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
            InitializeComponent();
            DatePickerFirst.SelectedDate = DateTime.Now.Date;
            DatePickerSecond.SelectedDate = DateTime.Now.Date;
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
        }

        private void Buttion_Start_analyzing_Click(object sender, RoutedEventArgs e)
        {
            string _login = TextBox_Login.Text;
            DateTime? _first_date = DatePickerFirst.SelectedDate;
            DateTime? _seconddate = DatePickerSecond.SelectedDate;
            Validation valid = new Validation();
            if (valid.IsValid(_first_date) && valid.IsValid(_seconddate))
            {
                //здесь отсылание данных Маше
                OutputData outputdatewindow = new OutputData();
                outputdatewindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные данных в поле Дата");
            }
        }
    }
}