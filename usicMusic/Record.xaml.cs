using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        string str = "";
        public Recorder(string str)
        {
            InitializeComponent();
            this.str = str;
        }

        Boolean isStart = true;
        RecordWithWaveIn rwa = new RecordWithWaveIn();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isStart)
            {
                startOrStop.Content = "stop";
                //record.StartRecord();
                rwa.StartRecord(str);
            }
            else
            {
                startOrStop.Content = "start";
                //record.StopRecord();
                rwa.StopRecord();
            }
            isStart = !isStart;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
             // 리소스 어디서 가져오지? 어디서 가져오지?
                                 // 리소스 어디서 가져오지? 어디서 가져오지?ㄱㄷ
        }


    }
}
