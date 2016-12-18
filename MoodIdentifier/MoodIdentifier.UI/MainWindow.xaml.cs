﻿using Newtonsoft.Json;
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
            animationTimer.Interval = new System.TimeSpan(0, 0, 0, 0, 70);
            ChangePlace();
            InitializeComponent();
        }

        private async void Buttion_Start_analyzing_Click(object sender, RoutedEventArgs e)
        {
            //TextBox_Login.ToolTip += TextBox_Login.Text;
            string login = '@' + TextBox_Login.Text;
            DateTime? firstdate = DatePickerFirst.SelectedDate;
            DateTime? seconddate = DatePickerSecond.SelectedDate;
            Validation valid = new Validation();
            
            if (valid.IsValid(firstdate,seconddate))
            {
                seconddate = seconddate.Value.AddDays(1);
                if (valid.IsValid(login))
                {
                    try
                    {
                        RepositoryTweetData rtd = new RepositoryTweetData();
                        RepositoryAnalysisData rad = new RepositoryAnalysisData();
                        Start();
                        //Downloading tweets
                        Dictionary<DateTime, List<string>> tweets = new Dictionary<DateTime, List<string>>();
                        tweets = rtd.GetTweets(login, (DateTime)firstdate, (DateTime)seconddate);

                        //Downloading emotion of each tweet
                        var analyzed = await rad.GetAnswer2(tweets);

                    OutputData outputDataWindow = new OutputData(analyzed,(DateTime)firstdate,(DateTime)seconddate);
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

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    Stop();
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