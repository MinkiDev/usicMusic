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

namespace usicMusic
{
    /// <summary>
    /// Recorder.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Recorder : Window
    {
        Record record = new Record();

        public Recorder()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            record.StartRecord();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            record.StopRecord(); // 리소스 어디서 가져오지? 어디서 가져오지?
                                 // 리소스 어디서 가져오지? 어디서 가져오지?ㄱㄷ
        }


    }
}
