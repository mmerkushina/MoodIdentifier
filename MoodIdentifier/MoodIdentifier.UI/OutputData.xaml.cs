﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoodIdentifier.UI
{
    /// <summary>
    /// Interaction logic for OutputData.xaml
    /// </summary>
    public partial class OutputData : Window
    {
        public delegate void DelegateForOutputDataClosed();
        public event DelegateForOutputDataClosed EventOutputDataClosed;
        List<EmotionPicture> emotionPictures = new List<EmotionPicture>();

        public OutputData()
        {
            emotionPictures.Add(new EmotionPicture { ImageFilePath = new Uri(System.IO.Path.GetFullPath(@"Pictures\anger.png")) });
            emotionPictures.Add(new EmotionPicture { ImageFilePath = new Uri(System.IO.Path.GetFullPath(@"Pictures\disgust.png")) });
            emotionPictures.Add(new EmotionPicture { ImageFilePath = new Uri(System.IO.Path.GetFullPath(@"Pictures\fear.png")) });
            emotionPictures.Add(new EmotionPicture { ImageFilePath = new Uri(System.IO.Path.GetFullPath(@"Pictures\joy.png")) });
            emotionPictures.Add(new EmotionPicture { ImageFilePath = new Uri(System.IO.Path.GetFullPath(@"Pictures\sadness.png")) });
            InitializeComponent();
        }

        private void Button_Back_To_MainWindow_Click(object sender, RoutedEventArgs e)
        {
            EventOutputDataClosed?.Invoke();
            this.Close();
        }

        private void dataGridOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataToOutput selected = (DataToOutput)dataGridOutput.SelectedItem;
            textBoxInfoMainEmotion.Text = string.Format("{0} is the main emotion of the day.",selected.MainEmotion);
            ImageSource source;
            switch (selected.MainEmotion)
            {
                case "Anger":
                    imageEmotion.Source = new BitmapImage(new Uri("Pictures/anger.png", UriKind.Relative));
                    break;
                case "Disgust":
                    imageEmotion.Source = new BitmapImage(new Uri("Pictures/disgust.png", UriKind.Relative));
                    break;
                case "Fear":
                    imageEmotion.Source = new BitmapImage(new Uri("Pictures/fear.png", UriKind.Relative));
                    break;
                case "Joy":
                    imageEmotion.Source = new BitmapImage(new Uri("Pictures/joy.png",UriKind.Relative));
                    break;
                case "Sadness":
                    imageEmotion.Source = new BitmapImage(new Uri("Pictures/sadness.png", UriKind.Relative));
                    break;
            }
        }
    }
}
