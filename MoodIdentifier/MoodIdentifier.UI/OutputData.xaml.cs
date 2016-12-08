using System;
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
        public OutputData()
        {
            InitializeComponent();
        }

        private void Button_Back_To_MainWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
