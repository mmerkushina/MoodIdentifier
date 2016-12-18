﻿using Microsoft.Research.DynamicDataDisplay;
using MoodIdentifier.AnalysisData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace MoodIdentifier.UI
{
    /// <summary>
    /// Interaction logic for OutputData.xaml
    /// </summary>
    public partial class OutputData : Window
    {
        public delegate void DelegateForOutputDataClosed();
        public event DelegateForOutputDataClosed EventOutputDataClosed;

        List<DataToOutput> Datalist { get; set; }
        Dictionary<DateTime, ClassForAnalysis> Dataframe { get; set; }

        public OutputData(Dictionary<DateTime,ClassForAnalysis> dataframe)
        {
            Converter convert = new Converter();
            Dataframe = dataframe;
            Datalist = convert.FromDictionaryToList(dataframe);
            InitializeComponent();
            dataGridOutput.ItemsSource = Datalist;
            textBoxInfoMainEmotion.Visibility = Visibility.Hidden;
            plot.Visibility = Visibility.Hidden;
        }

        private void Button_Back_To_MainWindow_Click(object sender, RoutedEventArgs e)
        {
            EventOutputDataClosed?.Invoke();
            this.Close();
        }

        private void dataGridOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            plot.Visibility = Visibility.Visible;
            textBoxInfoMainEmotion.Visibility = Visibility.Visible;
            DataToOutput selected = (DataToOutput)dataGridOutput.SelectedItem;
            textBoxInfoMainEmotion.Text = string.Format("{0} is the main emotion of {1}.",selected.MainEmotion,selected.Date);
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

            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }
            Excel.Workbook workBook;
            Excel.Worksheet worksheet;
            object temp = System.Reflection.Missing.Value;
            workBook = xlApp.Workbooks.Add(temp);
            worksheet = (Excel.Worksheet)workBook.Worksheets.Item[1];
            var result = (from analyzedKey in Dataframe.Keys
                         where analyzedKey.ToString("d") == selected.Date
                         select analyzedKey).ToList();
            var analyzedone = Dataframe[result[0]];
            for (int i = 0; i < 5; i++)
            {
                worksheet.Cells[1, 1] = "Anger";
                worksheet.Cells[1, 2] = "Disgust";
                worksheet.Cells[1, 3] = "Fear";
                worksheet.Cells[1, 4] = "Joy";
                worksheet.Cells[1, 5] = "Sadness";
                worksheet.Cells[2, 1] = analyzedone.AllEmotion.Anger;
                worksheet.Cells[2, 2] = analyzedone.AllEmotion.Disgust;
                worksheet.Cells[2, 3] = analyzedone.AllEmotion.Fear;
                worksheet.Cells[2, 4] = analyzedone.AllEmotion.Joy;
                worksheet.Cells[2, 5] = analyzedone.AllEmotion.Sadness;

            }
            workBook.SaveAs("excel_for_graph.xls", Excel.XlFileFormat.xlWorkbookNormal, temp, temp, temp, temp, Excel.XlSaveAsAccessMode.xlExclusive, temp,temp,temp, temp, temp);
            workBook.Close(true, temp, temp);
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(workBook);
            Marshal.ReleaseComObject(xlApp);

            //xlWorkBook = xlApp.Workbooks.Add(temp);

        }

        private void dataGridOutput_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dataGridOutput.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void labelQuery_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
