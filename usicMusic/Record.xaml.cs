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
    public partial class Record : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private static Random rnd = new Random();
        private static double audioValueMax = 0;
        private static double audioValueLast = 0;
        private static int audioCount = 0;
        private static int RATE = 44100;
        private static int BUFFER_SAMPLES = 1024;

        string str = "";
        public Record(string str)
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
                Start();
            }
            else
            {
                Stop();
            }
            isStart = !isStart;
        }

        private void OnDataAvailable(object sender, WaveInEventArgs args)
        {
            float max = 0;

            // interpret as 16 bit audio
            for (int index = 0; index < args.BytesRecorded; index += 2)
            {
                short sample = (short)((args.Buffer[index + 1] << 8) |
                                        args.Buffer[index + 0]);
                var sample32 = sample / 32768f; // to floating point
                if (sample32 < 0) sample32 = -sample32; // absolute value 
                if (sample32 > max) max = sample32; // is this the max value?
            }

            // calculate what fraction this peak is of previous peaks
            if (max > audioValueMax)
            {
                audioValueMax = (double)max;
            }
            audioValueLast = max;
            audioCount += 1;
        }

        private void Start()
        {
            startOrStop.Content = "stop";
            rwa.StartRecord(str);

            var waveIn = new WaveInEvent();
            waveIn.DeviceNumber = 0;
            waveIn.WaveFormat = new NAudio.Wave.WaveFormat(RATE, 1);
            waveIn.DataAvailable += OnDataAvailable;
            waveIn.BufferMilliseconds = (int)((double)BUFFER_SAMPLES / (double)RATE * 1000.0);
            waveIn.StartRecording();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
        }

        private void Stop()
        {
            rwa.StopRecord();
            dispatcherTimer.Stop();
            progressBar.Value = 0;

            startOrStop.Content = "start";
            textbox.Content = "00.00% peak";
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            double frac = audioValueLast / audioValueMax;
            progressBar.Value = 100 * frac;
            textbox.Content = string.Format("{0:00.00}% peak", progressBar.Value);
        }

    }
}
